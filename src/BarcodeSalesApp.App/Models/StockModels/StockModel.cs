namespace BarcodeSalesApp.App.Models.StockModels;

public class StockModel
{
  public long ProductId { get; set; }
  public string Name { get; set; } = null!;
  public int? UnitsPerCase { get; set; }
  public int QuantityInStock { get; set; }
}
