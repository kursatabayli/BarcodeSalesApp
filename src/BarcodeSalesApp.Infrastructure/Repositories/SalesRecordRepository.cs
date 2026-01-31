using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Domain.Entities;
using BarcodeSalesApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure.Repositories;

public class SalesRecordRepository : GenericRepository<SalesRecordEntity>, ISalesRecordRepository
{
  public SalesRecordRepository(AppDbContext context) : base(context)
  {
  }
  public IQueryable<SalesRecordEntity> GetAllRecordsByDate(DateOnly saleDate)
  => _dbSet.Where(sr => sr.SaleDate == saleDate).Include(sr => sr.Product).AsNoTracking();

  public async Task<DateTime?> GetEarliestSalesRecordDateAsync()
  {
    var earliestDateOnly = await _dbSet.Select(sr => (DateOnly?)sr.SaleDate).MinAsync();

    return earliestDateOnly?.ToDateTime(TimeOnly.MinValue);
  }

  public async Task<IEnumerable<SalesRecordEntity>> GetRecordsByDateAsync(DateOnly saleDate)
  {
    var records = await _dbSet
        .Where(sr => sr.SaleDate == saleDate)
        .Include(sr => sr.Product)
        .AsNoTracking()
        .ToListAsync();

    return records;
  }

  public async Task<List<SalesRecordEntity>> GetExistingRecordsAsync(DateOnly saleDate, IEnumerable<long> productIds, CancellationToken cancellationToken = default)
  => await _dbSet.Where(sr => sr.SaleDate == saleDate && productIds.Contains(sr.ProductId)).Include(sr => sr.Product).ToListAsync(cancellationToken);
}
