using Barkod.Helpers;
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
    public class SalesService
    {
        private readonly ISalesRecordRepository _salesRecordRepository;

        public SalesService(ISalesRecordRepository salesRecordRepository) => _salesRecordRepository = salesRecordRepository;

        public async Task CompleteSale(List<CartItem> cart)
        {
            foreach (var item in cart)
            {
                var existingRecord = await _salesRecordRepository.GetByBarcodeWithDateAsync(item.Barcode, DateTime.Today.ToString("yyyy-MM-dd"));
                if (existingRecord != null)
                {
                    existingRecord.QuantitySold += item.Quantity;
                    await _salesRecordRepository.Update(existingRecord.Id, existingRecord);
                }
                else
                {
                    var salesRecord = new SalesRecord
                    {
                        Barcode = item.Barcode,
                        Name = item.Name,
                        QuantitySold = item.Quantity,
                        SalePrice = item.SalePrice,
                        PurchasePrice = item.PurchasePrice,
                        Profit = item.Profit,
                        SaleDate = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                    };
                    await _salesRecordRepository.Create(salesRecord);
                }
            }
        }
    }
}
