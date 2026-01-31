namespace BarcodeSalesApp.Domain.Entities;

public class SalesRecordEntity
{
        public long Id { get; private set; }
        public long ProductId { get; private set; }
        public ProductEntity Product { get; private set; } = null!;
        public int QuantitySold { get; private set; }
        public decimal SalePrice { get; private set; }
        public DateOnly SaleDate { get; private set; }
        public decimal TotalSales { get; private set; }
        public decimal TotalProfit { get; private set; }

        private SalesRecordEntity() { }

        private SalesRecordEntity(ProductEntity product, int quantitySold)
        {
                ProductId = product.Id;
                QuantitySold = quantitySold;
                SalePrice = product.SalePrice;
                SaleDate = DateOnly.FromDateTime(DateTime.Now);
                TotalSales = product.SalePrice * quantitySold;
                TotalProfit = (product.SalePrice - product.PurchasePrice) * quantitySold;
        }

        public static SalesRecordEntity Add(ProductEntity product, int quantitySold)
        {
                return new SalesRecordEntity(product, quantitySold);
        }

        public void IncreaseQuantity(int quantity)
        {
                QuantitySold += quantity;
                TotalSales = SalePrice * QuantitySold;
                TotalProfit = (SalePrice - Product.PurchasePrice) * QuantitySold;
        }
}
