using Microsoft.AspNetCore.Components;
using Barkod.Services;
using MudBlazor;
using Microsoft.AspNetCore.Components.Web;
using Barkod.Interfaces;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;
using Barkod.Constants;
using Barkod.Helpers;

namespace Barkod.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] public ProductService ProductService { get; set; }
        [Inject] public SalesService SalesService { get; set; }
        [Inject] public IShortcutService ShortcutService { get; set; }
        [Inject] public INavigationService NavigationService { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }
        [Inject] public FocusManager _focusManager { get; set; }

        private ShortcutOption currentShortcut = new();
        public readonly CartManager _cartManager = new();

        public MudNumericField<long?> barcodeField;
        public long? scannedBarcode;
        public bool _isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            ShortcutService.LoadShortcuts();
            currentShortcut = ShortcutService.Shortcut;
        }
        public async Task HandleBarcodeKeyDown(KeyboardEventArgs e)
        {
            if (e.Key.ToLower() == currentShortcut.SaleKey && !e.CtrlKey && !e.AltKey && !e.ShiftKey)
            {
                await CompleteSaleAsync();
            }
            else if (e.Key.ToLower() == currentShortcut.ClearKey && !e.CtrlKey && !e.AltKey && !e.ShiftKey)
            {
                _cartManager.ClearCart();
                StateHasChanged();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_isInitialized)
            {
                _isInitialized = true;
                _focusManager.Initialize(barcodeField);
                await _focusManager.FocusInputAsync();
            }
        }

        public async Task OnBarcodeScanned()
        {
            if (scannedBarcode.HasValue)
            {
                var product = await ProductService.GetProductByBarcode(scannedBarcode.Value);
                if (product != null)
                {
                    _cartManager.AddProduct(new CartItem
                    {
                        Barcode = product.Barcode,
                        Name = product.Name,
                        SalePrice = product.SalePrice,
                        PurchasePrice = product.PurchasePrice,
                        Profit = product.Profit,
                        Quantity = 1
                    });

                    scannedBarcode = null;
                    await _focusManager.BlurInputAsync();
                }
            }
        }

        public async Task CompleteSaleAsync()
        {
            await SalesService.CompleteSale(_cartManager.Cart);
            _cartManager.ClearCart();
            await InvokeAsync(StateHasChanged);
        }

        public void NavigatePage(string url) => NavigationService.NavigateTo(url);

    }
}
