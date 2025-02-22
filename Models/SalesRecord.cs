using SQLite;
using System;

namespace Barkod.Models
{
    public class SalesRecord : BaseEntity
    {
        public long Barcode { get; set; }
        public int QuantitySold { get; set; }
        public string SaleDate { get; set; }
        public decimal TotalSales => SalePrice * QuantitySold;
        public decimal TotalProfit => Profit * QuantitySold;
    }
}
