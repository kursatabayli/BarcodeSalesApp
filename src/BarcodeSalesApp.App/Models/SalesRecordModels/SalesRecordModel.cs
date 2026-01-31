namespace BarcodeSalesApp.App.Models.SalesRecordModels;

public class SalesRecordModel
{
  public long Id { get; set; }
  public long ProductId { get; set; }
  public required string Name { get; set; }
  public string? Barcode { get; set; }
  public decimal SalePrice { get; set; }
  public int QuantitySold { get; set; }
  public DateOnly SaleDate { get; set; }
  public decimal TotalSales { get; set; }
  public decimal TotalProfit { get; set; }
}
