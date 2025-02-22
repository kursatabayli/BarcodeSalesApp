using SQLite;

namespace Barkod.Models
{
    public class Product : BaseEntity
    {
        [Unique]
        public long Barcode { get; set; }
        public int StockUnit { get; set; }
        public int StockMultiplier { get; set; }
        public int StockQuantity { get; set; }

    }
}
