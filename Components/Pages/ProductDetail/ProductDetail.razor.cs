using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using System.Threading.Tasks;
using MudBlazor;
using Barkod.Helpers;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;

namespace Barkod.Components.Pages.ProductDetail
{
    public partial class ProductDetail : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public INavigationService NavigationService { get; set; }
        public Product product { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }

        protected override async Task OnInitializedAsync() => product = await ProductRepository.GetById(Id);

        public void NavigatePage(string url) => NavigationService.NavigateTo(url);

        public async Task DeleteItemAsync()
        {
            var title = Localizer["DeleteProductTitle", product.Name];

            var parameters = new DialogParameters<DeleteItem>
                    {
                        { x => x.ContentText, Localizer["DeleteProductConfirmText"] },
                        { x => x.Title, title },
                        { x => x.SubmitButton, Localizer["Delete"] }
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
                    await ProductRepository.Delete(Id);
                    Snackbar.Add(Localizer["DeleteProductSuccess"], Severity.Success);
                    NavigationService.NavigateTo("/product-list");
                }
                catch (Exception ex)
                {
                    Snackbar.Add(Localizer["DeleteProductError"], Severity.Error);
                }
            }
        }
    }
}
