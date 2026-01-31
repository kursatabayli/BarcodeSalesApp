namespace BarcodeSalesApp.Domain.Entities;

public class ProductEntity
{
        public long Id { get; private set; }
        public string Name { get; private set; } = null!;
        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public bool IsBarcoded { get; private set; }
        public string? Barcode { get; private set; }
        public int? UnitsPerCase { get; private set; }
        public StockEntity Stock { get; private set; } = null!;
        public ICollection<SalesRecordEntity> SalesRecords { get; private set; } = [];

        private ProductEntity() { }
        private ProductEntity(
                string name,
                decimal purchasePrice,
                decimal salePrice,
                string? barcode,
                int? unitsPerCase)
        {
                Name = name;
                PurchasePrice = purchasePrice;
                SalePrice = salePrice;
                Barcode = barcode;
                IsBarcoded = !string.IsNullOrEmpty(barcode);
                UnitsPerCase = unitsPerCase;
                Stock = new StockEntity();
        }
        public static ProductEntity Add(
                string name,
                decimal purchasePrice,
                decimal salePrice,
                string? barcode,
                int? unitsPerCase)
        {
                return new ProductEntity(
                name,
                purchasePrice,
                salePrice,
                barcode,
                unitsPerCase);
        }
        public void Update(
                string name,
                decimal purchasePrice,
                decimal salePrice,
                string? barcode,
                int? unitsPerCase)
        {
                Name = name;
                PurchasePrice = purchasePrice;
                SalePrice = salePrice;
                Barcode = barcode;
                IsBarcoded = !string.IsNullOrEmpty(barcode);
                UnitsPerCase = unitsPerCase;
        }
}
