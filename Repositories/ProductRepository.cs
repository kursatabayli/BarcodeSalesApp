using Barkod.Interfaces;
using Barkod.Models;
using SQLite;


namespace Barkod.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public ProductRepository(SQLiteAsyncConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<Product> GetByBarcodeAsync(long? barcode)
        {
            return await _connection.Table<Product>().Where(p => p.Barcode == barcode).FirstOrDefaultAsync();
        }
    }
}
