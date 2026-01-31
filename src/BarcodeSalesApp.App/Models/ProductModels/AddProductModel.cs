namespace BarcodeSalesApp.App.Models.ProductModels;

public class AddProductModel
{
  public string Name { get; set; } = null!;
  public decimal PurchasePrice { get; set; }
  public decimal SalePrice { get; set; }
  public bool IsBarcoded { get; set; }
  public string? Barcode { get; set; }
  public int? UnitsPerCase { get; set; }
}
