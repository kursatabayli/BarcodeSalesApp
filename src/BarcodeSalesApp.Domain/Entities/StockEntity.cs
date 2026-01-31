namespace BarcodeSalesApp.Domain.Entities;

public class StockEntity
{
  public long ProductId { get; private set; }
  public ProductEntity Product { get; private set; } = null!;
  public int QuantityInStock { get; private set; }
  public StockEntity()
  {
    QuantityInStock = 0;
  }

  public void UpdateStock(int newQuantity)
  {
    if (newQuantity < 0)
    {
      throw new ArgumentException("Stok miktarı negatif olamaz.", nameof(newQuantity));
    }
    QuantityInStock = newQuantity;
  }

  public void RemoveStock(int amount)
  {
    if (amount <= 0)
      throw new ArgumentException("Çıkarılacak miktar pozitif olmalıdır.", nameof(amount));

    if (QuantityInStock < amount)
      QuantityInStock = 0;
    else
      QuantityInStock -= amount;
  }
}
