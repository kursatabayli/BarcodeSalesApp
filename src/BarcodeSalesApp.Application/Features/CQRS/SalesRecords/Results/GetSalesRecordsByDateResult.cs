
using BarcodeSalesApp.Application.Features.Helpers;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;

public class GetSalesRecordsByDateResult
{
  public PagedResult<SalesRecordResult> PagedResult { get; set; } = new();
  public decimal GrandTotalSales { get; set; }
  public decimal GrandTotalProfit { get; set; }
}
