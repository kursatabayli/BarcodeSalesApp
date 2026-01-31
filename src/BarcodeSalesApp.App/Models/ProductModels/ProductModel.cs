namespace BarcodeSalesApp.App.Models.ProductModels;

public class ProductModel
{
  public long Id { get; set; }
  public string Name { get; set; } = null!;
  public decimal PurchasePrice { get; set; }
  public decimal SalePrice { get; set; }
  public bool IsBarcoded { get; set; }
  public string? Barcode { get; set; }
  public int? UnitsPerCase { get; set; }
  public int? QuantityInStock { get; set; }
}
