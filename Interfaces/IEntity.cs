namespace Barkod.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal PurchasePrice { get; set; }
        decimal SalePrice { get; set; }
        decimal Profit { get; set; }

    }
}
