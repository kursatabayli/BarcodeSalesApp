namespace BarcodeSalesApp.Contracts.Services;

public interface ISalesService
{
  Task<bool> CreateSaleAsync(IList<CartItemHelper> cartItemHelpers);
}

public class CartItemHelper
{
  public long ProductId { get; set; }
  public int Quantity { get; set; }
}