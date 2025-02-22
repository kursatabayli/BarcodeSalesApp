using Microsoft.AspNetCore.Components;
using Barkod.Models;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;
using Barkod.Services;
using Barkod.Helpers;
using System.Globalization;

namespace Barkod.Components.Pages.CashOut
{
    public partial class CashOut : ComponentBase
    {
        [Inject] public SalesReportService SalesReportService { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }
        public List<SalesRecord> DailySalesRecords { get; private set; } = new();
        public decimal TotalSales { get; private set; }
        public decimal TotalProfit { get; private set; }
        public DateOnly SelectedDate { get; set; }
        public List<DateOnly> AvailableDates { get; private set; } = new();
        public string SearchText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AvailableDates = await SalesReportService.GetAvailableDatesAsync();
            if (AvailableDates.Any() && AvailableDates.Max() == DateOnly.FromDateTime(DateTime.Today))
            {
                SelectedDate = AvailableDates.Max();
                await LoadDailySales();
            }
            else
            {
                SelectedDate = DateOnly.FromDateTime(DateTime.Today);
            }

        }

        public async Task LoadDailySales()
        {
            string selectedDateString = SelectedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(selectedDateString))
            {
                DailySalesRecords = await SalesReportService.GetDailySalesAsync(selectedDateString);
                TotalSales = SalesReportService.CalculateTotalSales(DailySalesRecords);
                TotalProfit = SalesReportService.CalculateTotalProfit(DailySalesRecords);
            }
        }

        public IEnumerable<SalesRecord> FilteredProducts =>
            DailySalesRecords.FilterBySearchText(SearchText);
    }
}