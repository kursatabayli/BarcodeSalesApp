using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Domain.Entities;
using BarcodeSalesApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
  public ProductRepository(AppDbContext context) : base(context) { }

  public IQueryable<ProductEntity> GetAllProductsWithStock() => _dbSet.Include(p => p.Stock).AsNoTracking();

  public async Task<ProductEntity?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
    => await FindAsNoTracking(p => p.Barcode == barcode).FirstOrDefaultAsync(cancellationToken);
}
