using BarcodeSalesApp.App.Models.ProductModels;

namespace BarcodeSalesApp.App.Models.CartModels;

public class CartItemModel
{
  public required ProductModel Product { get; set; }
  private int _quantity;
  public int Quantity
  {
    get => _quantity;
    set
    {
      _quantity = value;
      Total = Product.SalePrice * _quantity;
    }
  }

  public decimal Total { get; private set; }
}
