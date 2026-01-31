using BarcodeSalesApp.Domain.Entities;

namespace BarcodeSalesApp.Contracts.Repositories;

public interface ISalesRecordRepository : IRepository<SalesRecordEntity>
{
  Task<DateTime?> GetEarliestSalesRecordDateAsync();
  IQueryable<SalesRecordEntity> GetAllRecordsByDate(DateOnly saleDate);
  Task<IEnumerable<SalesRecordEntity>> GetRecordsByDateAsync(DateOnly saleDate);
  Task<List<SalesRecordEntity>> GetExistingRecordsAsync(DateOnly saleDate, IEnumerable<long> productIds, CancellationToken cancellationToken = default);
}
