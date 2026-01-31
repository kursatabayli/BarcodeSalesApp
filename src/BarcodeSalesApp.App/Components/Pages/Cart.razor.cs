using AutoMapper;
using BarcodeSalesApp.App.Components.Helpers;
using BarcodeSalesApp.App.Models.CartModels;
using BarcodeSalesApp.App.Models.ProductModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using BarcodeSalesApp.Contracts.Options;
using BarcodeSalesApp.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages;

public partial class Cart : ComponentBase, IDisposable
{
  [Inject] private IMediator Mediator { get; set; } = null!;
  [Inject] private IMapper Mapper { get; set; } = null!;
  [Inject] private ISalesService SalesService { get; set; } = null!;
  [Inject] private IShortcutService ShortcutService { get; set; } = null!;
  [Inject] private NavigationManager NavigationManager { get; set; } = null!;
  [Inject] private IStringLocalizer<AppStrings> Localizer { get; set; } = null!;
  [Inject] private ISnackbar Snackbar { get; set; } = null!;
  private readonly FocusManager _focusManager = new();
  private List<CartItemModel> CartItems { get; set; } = [];
  private ShortcutOption currentShortcut = null!;
  private MudNumericField<long?> barcodeField = null!;
  private long? scannedBarcode;
  private bool _isInitialized = false;

  protected override void OnInitialized()
  {
    currentShortcut = ShortcutService.Shortcut;
  }
  public async Task HandleBarcodeKeyDown(KeyboardEventArgs e)
  {
    if (e.Key.Equals(currentShortcut.SaleKey, StringComparison.CurrentCultureIgnoreCase) && !e.CtrlKey && !e.AltKey && !e.ShiftKey)
    {
      await CompleteSaleAsync();
    }
    else if (e.Key.Equals(currentShortcut.ClearKey, StringComparison.CurrentCultureIgnoreCase) && !e.CtrlKey && !e.AltKey && !e.ShiftKey)
    {
      CartItems.Clear();
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
    if (scannedBarcode == null) return;
    var product = await Mediator.Send(new GetProductByBarcodeQuery(scannedBarcode.Value.ToString()));
    if (product != null)
    {
      var existingCartItem = CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
      if (existingCartItem == null)
      {
        var cartItem = new CartItemModel
        {
          Product = Mapper.Map<ProductModel>(product),
          Quantity = 1
        };
        CartItems.Add(cartItem);
      }
      else
      {
        existingCartItem.Quantity++;
      }
    }
    else
    {
      Snackbar.Add(Localizer[AppStrings.ProductNotFound], Severity.Warning);
    }
    scannedBarcode = null;
    await InvokeAsync(StateHasChanged);
    await _focusManager.BlurInputAsync();
  }

  public async Task CompleteSaleAsync()
  {
    if (CartItems.Count > 0)
    {
      var cartItemHelpers = CartItems.Select(ci => new CartItemHelper
      {
        ProductId = ci.Product.Id,
        Quantity = ci.Quantity
      }).ToList();
      var result = await SalesService.CreateSaleAsync(cartItemHelpers);
      if (result)
      {
        CartItems.Clear();
        await InvokeAsync(StateHasChanged);
      }
      else
      {
        Snackbar.Add(Localizer[AppStrings.SaleFailed], Severity.Error);
      }
    }
    else
    {
      Snackbar.Add(Localizer[AppStrings.CartIsEmpty], Severity.Info);
    }
  }
  public void RemoveProduct(ProductModel product)
  {
    var item = CartItems.FirstOrDefault(p => p.Product.Id == product.Id);
    if (item != null)
    {
      if (item.Quantity > 1)
        item.Quantity--;
      else
        CartItems.Remove(item);
    }
  }
  public void NavigateTo(string url) => NavigationManager.NavigateTo(url);
  public void Dispose()
  {
    _focusManager.Dispose();
  }
}
