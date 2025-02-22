using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using System.Threading.Tasks;
using MudBlazor;
using Barkod.Helpers;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;

namespace Barkod.Components.Pages.EditStock
{
    public partial class EditStock : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public INavigationService NavigationService { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }

        public Product product { get; set; } = new();
        public int newStockUnit { get; set; }

        protected override async Task OnInitializedAsync() => product = await ProductRepository.GetById(Id);

        public async Task UpdateProductAsync()
        {
            if (product != null)
            {
                product.StockUnit += newStockUnit;
                product.StockQuantity = product.StockUnit * product.StockMultiplier;
                await ProductRepository.Update(product.Id, product);
                Snackbar.Add(Localizer["UpdateStockSuccess"], Severity.Success);
                NavigatePage($"/product-detail/{Id}");
            }
        }

        public async Task ResetStockAsync()
        {
            var parameters = new DialogParameters<DeleteItem>
                    {
                        { x => x.ContentText, Localizer["ResetStockConfirmText"] },
                        { x => x.Title, Localizer["ResetStockTitle"] },
                        { x => x.SubmitButton, Localizer["Reset"] }
                    };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = false,
                MaxWidth = MaxWidth.ExtraSmall,
                Position = DialogPosition.Center
            };

            var dialog = await DialogService.ShowAsync<DeleteItem>(null, parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                try
                {
                    product.StockUnit = 0;
                    product.StockQuantity = 0;
                    await ProductRepository.Update(product.Id, product);
                    StateHasChanged();
                    Snackbar.Add(Localizer["ResetStockSuccess"], Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add(Localizer["ResetStockError"], Severity.Error);
                }
            }
        }

        public void NavigatePage(string url) => NavigationService.NavigateTo(url);

    }
}
