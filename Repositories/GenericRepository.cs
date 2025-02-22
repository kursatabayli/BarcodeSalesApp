using Barkod.Models;
using Barkod.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barkod.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public GenericRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
            _connection.CreateTableAsync<TEntity>().Wait();
        }

        public async Task Create(TEntity entity)
        {
            await _connection.InsertAsync(entity);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _connection.FindAsync<TEntity>(id);
        }



        public async Task<List<TEntity>> GetAll()
        {
            return await _connection.Table<TEntity>().ToListAsync();
        }

        public async Task Update(int id, TEntity entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                await _connection.DeleteAsync(entity);
            }
        }
    }
}
