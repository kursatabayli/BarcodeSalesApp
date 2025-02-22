using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using System.Threading.Tasks;
using MudBlazor;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;

namespace Barkod.Components.Pages.EditProduct
{
    public partial class EditProduct : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public INavigationService NavigationService { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }
        public Product product { get; set; } = new();

        protected override async Task OnInitializedAsync() => product = await ProductRepository.GetById(Id);

        public async Task UpdateProductAsync()
        {
            if (product != null)
            {
                product.Profit = product.SalePrice - product.PurchasePrice;
                await ProductRepository.Update(product.Id, product);
                Snackbar.Add(Localizer["ProductUpdated"], Severity.Success);
                NavigatePage($"/product-detail/{Id}");
            }
        }

        public void NavigatePage(string url) => NavigationService.NavigateTo(url);

    }
}
