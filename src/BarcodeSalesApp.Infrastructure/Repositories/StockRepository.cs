using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Domain.Entities;
using BarcodeSalesApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure.Repositories;

public class StockRepository : GenericRepository<StockEntity>, IStockRepository
{
  public StockRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<StockEntity?> GetByProductIdAsync(long productId, CancellationToken cancellationToken = default)
    => await FindAsNoTracking(p => p.ProductId == productId).Include(s => s.Product).FirstOrDefaultAsync(cancellationToken);
  public async Task<List<StockEntity>> GetByProductIdsAsync(IEnumerable<long> productIds, CancellationToken cancellationToken = default)
    => await Find(s => productIds.Contains(s.ProductId)).Include(s => s.Product).ToListAsync(cancellationToken);
}
