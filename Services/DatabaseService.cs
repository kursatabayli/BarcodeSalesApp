using Barkod.Models;
using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Barkod.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "BarkodDB.db3");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }

        public async Task InitializeAsync()
        {
            await _database.CreateTableAsync<Product>();
            await _database.CreateTableAsync<SalesRecord>();
        }
    }
}
