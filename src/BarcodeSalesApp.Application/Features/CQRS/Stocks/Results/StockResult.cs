namespace BarcodeSalesApp.Application.Features.CQRS.Stocks.Results;

public class StockResult
{
  public long ProductId { get; set; }
  public string Name { get; set; }
  public int? UnitsPerCase { get; set; }
  public int QuantityInStock { get; set; }
}
