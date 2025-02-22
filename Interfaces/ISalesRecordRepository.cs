using Barkod.Models;

namespace Barkod.Interfaces
{
    public interface ISalesRecordRepository : IRepository<SalesRecord>
    {
        Task<decimal> GetTotalSalesAsync();
        Task<decimal> GetTotalProfitAsync();
        Task<SalesRecord> GetByBarcodeWithDateAsync(long barcode, string date);
        Task<List<SalesRecord>> GetSalesByDateAsync(string dateTime);
        Task<List<string>> GetUniqueSaleDates();
        Task ClearAllSales();
    }
}
