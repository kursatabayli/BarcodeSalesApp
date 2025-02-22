using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using System.Threading.Tasks;
using System.Globalization;
using MudBlazor;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;

namespace Barkod.Components.Pages.AddProduct
{
    public partial class AddProduct : ComponentBase
    {
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }
        public Product product { get; set; } = new();

        public CultureInfo _culture = CultureInfo.CurrentCulture;


        public async Task SaveProductAsync()
        {

            var existingProduct = await ProductRepository.GetByBarcodeAsync(product.Barcode);

            if (existingProduct != null)
            {
                var message = string.Format(Localizer["ProductExists"], existingProduct.Barcode, existingProduct.Name);
                Snackbar.Add(message, Severity.Error);
                return;
            }

            try
            {
                product.Profit = product.SalePrice - product.PurchasePrice;
                await ProductRepository.Create(product);

                Snackbar.Add(Localizer["NewProduct"], Severity.Success);
                product = new Product();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Kayıt hatası: {ex.Message}", Severity.Error);
            }

            StateHasChanged();
        }
    }
}