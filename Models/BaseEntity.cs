using Barkod.Interfaces;
using SQLite;

namespace Barkod.Models
{
    public class BaseEntity : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Profit { get; set; }

    }
}
