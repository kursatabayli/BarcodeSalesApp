using Barkod.Interfaces;
using Barkod.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Barkod.Repositories
{
    public class SalesRecordRepository : GenericRepository<SalesRecord>, ISalesRecordRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public SalesRecordRepository(SQLiteAsyncConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            var salesRecords = await _connection.Table<SalesRecord>().ToListAsync();
            decimal totalSales = salesRecords.Sum(s => s.SalePrice * s.QuantitySold);
            return totalSales;
        }

        public async Task<decimal> GetTotalProfitAsync()
        {
            var salesRecords = await _connection.Table<SalesRecord>().ToListAsync();
            decimal totalProfit = salesRecords.Sum(s => s.Profit * s.QuantitySold);
            return totalProfit;
        }

        public async Task<List<SalesRecord>> GetSalesByDateAsync(string dateTime)
        {
            return await _connection.Table<SalesRecord>().Where(r => r.SaleDate == dateTime).ToListAsync();
        }

        public async Task<SalesRecord> GetByBarcodeWithDateAsync(long barcode, string date)
        {
            return await _connection.Table<SalesRecord>().Where(p => p.Barcode == barcode && p.SaleDate == date).FirstOrDefaultAsync();
        }

        public async Task ClearAllSales()
        {
            await _connection.DropTableAsync<SalesRecord>();
            await _connection.CreateTableAsync<SalesRecord>();
        }

        public async Task<List<string>> GetUniqueSaleDates()
        {
            var salesRecords = await _connection.Table<SalesRecord>().ToListAsync();
            return salesRecords.Select(r => r.SaleDate).Distinct().ToList();
        }
    }
}
