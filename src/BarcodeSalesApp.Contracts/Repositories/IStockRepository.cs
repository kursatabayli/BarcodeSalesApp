using BarcodeSalesApp.Domain.Entities;

namespace BarcodeSalesApp.Contracts.Repositories;

public interface IStockRepository : IRepository<StockEntity>
{
  Task<StockEntity?> GetByProductIdAsync(long productId, CancellationToken cancellationToken = default);
  Task<List<StockEntity>> GetByProductIdsAsync(IEnumerable<long> productIds, CancellationToken cancellationToken = default);
}
