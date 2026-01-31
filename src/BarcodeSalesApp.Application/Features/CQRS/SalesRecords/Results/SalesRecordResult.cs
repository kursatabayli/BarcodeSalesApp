namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;

public class SalesRecordResult
{
  public long Id { get; set; }
  public long ProductId { get; set; }
  public string Name { get; set; }
  public string? Barcode { get; set; }
  public decimal SalePrice { get; set; }
  public int QuantitySold { get; set; }
  public DateOnly SaleDate { get; set; }
  public decimal TotalSales { get; set; }
  public decimal TotalProfit { get; set; }
}
