using Barkod.Models;

namespace Barkod.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByBarcodeAsync(long? barcode);

    }
}
