using System.Globalization;
using AutoMapper;
using BarcodeSalesApp.App.Models.ProductModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Products;

public partial class AddProduct : ComponentBase
{
  [Inject] public IMediator Mediator { get; set; } = default!;
  [Inject] public IMapper Mapper { get; set; } = default!;
  [Inject] public ISnackbar Snackbar { get; set; } = default!;
  [Inject] public NavigationManager NavigationManager { get; set; } = default!;
  [Inject] public IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  public AddProductModel addProductModel { get; set; } = new();

  public CultureInfo _culture = CultureInfo.CurrentCulture;


  public async Task SaveProductAsync()
  {

    var existingProduct = await Mediator.Send(new GetProductByBarcodeQuery(addProductModel.Barcode));

    if (existingProduct != null)
    {
      var message = string.Format(Localizer[AppStrings.ProductExists], existingProduct.Barcode, existingProduct.Name);
      Snackbar.Add(message, Severity.Warning);
      return;
    }

    try
    {
      var newProduct = Mapper.Map<AddProductCommand>(addProductModel);
      var result = await Mediator.Send(newProduct);

      Snackbar.Add(Localizer[AppStrings.NewProduct], Severity.Success);
      addProductModel = new AddProductModel();
    }
    catch (Exception ex)
    {
      Snackbar.Add($"Kayıt hatası: {ex.Message}", Severity.Error);
    }

    StateHasChanged();
  }

  public void Back() => NavigationManager.NavigateTo("/product-list");
}
