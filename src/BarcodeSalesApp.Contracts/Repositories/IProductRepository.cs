using BarcodeSalesApp.Domain.Entities;

namespace BarcodeSalesApp.Contracts.Repositories;

public interface IProductRepository : IRepository<ProductEntity>
{
  IQueryable<ProductEntity> GetAllProductsWithStock();
  Task<ProductEntity?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
}
