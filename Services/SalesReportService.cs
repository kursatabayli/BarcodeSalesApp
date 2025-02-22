using Barkod.Interfaces;
using Barkod.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Services
{
    public class SalesReportService
    {
        private readonly ISalesRecordRepository _salesRecordRepository;

        public SalesReportService(ISalesRecordRepository salesRecordRepository)
            => _salesRecordRepository = salesRecordRepository;

        public async Task<List<SalesRecord>> GetDailySalesAsync(string date)
            => await _salesRecordRepository.GetSalesByDateAsync(date);

        public decimal CalculateTotalSales(List<SalesRecord> records)
            => records.Sum(r => r.TotalSales);

        public decimal CalculateTotalProfit(List<SalesRecord> records)
            => records.Sum(r => r.TotalProfit);

        public async Task<List<DateOnly>> GetAvailableDatesAsync()
        {
            var dateStrings = await _salesRecordRepository.GetUniqueSaleDates();
            var dates = new List<DateOnly>(dateStrings.Count);

            foreach (var dateStr in dateStrings)
            {
                if (DateOnly.TryParseExact(
                    dateStr,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var date))
                {
                    dates.Add(date);
                }
            }

            return dates.OrderByDescending(d => d).ToList();

        }
    }
}
