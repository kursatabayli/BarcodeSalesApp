using AutoMapper;
using BarcodeSalesApp.App.Components.Helpers;
using BarcodeSalesApp.App.Models.ProductModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Products;

public partial class ProductDetail : ComponentBase
{
  [Parameter] public long Id { get; set; }
  [Inject] public IMediator Mediator { get; set; } = default!;
  [Inject] public IMapper Mapper { get; set; } = default!;
  [Inject] public NavigationManager NavigationManager { get; set; } = default!;
  public ProductModel productModel { get; set; } = default!;
  [Inject] public IDialogService DialogService { get; set; } = default!;
  [Inject] public ISnackbar Snackbar { get; set; } = default!;
  [Inject] public IStringLocalizer<AppStrings> Localizer { get; set; } = default!;

  protected override async Task OnInitializedAsync()
  {
    var productResult = await Mediator.Send(new GetProductByIdQuery(Id));
    productModel = Mapper.Map<ProductModel>(productResult);
  }


  public void NavigatePage(string url) => NavigationManager.NavigateTo(url);

  public async Task DeleteItemAsync()
  {
    var title = Localizer[AppStrings.DeleteProductTitle, productModel.Name];

    var parameters = new DialogParameters<DeleteItem>
                    {
                        { x => x.ContentText, Localizer[AppStrings.DeleteProductConfirmText] },
                        { x => x.Title, title },
                        { x => x.SubmitButton, Localizer[AppStrings.Delete] }
                    };

    var options = new DialogOptions
    {
      CloseOnEscapeKey = false,
      MaxWidth = MaxWidth.ExtraSmall,
      Position = DialogPosition.Center
    };

    var dialog = await DialogService.ShowAsync<DeleteItem>(null, parameters, options);
    var result = await dialog.Result;

    if (result is not null && !result.Canceled)
    {
      try
      {
        await Mediator.Send(new DeleteProductCommand(Id));
        Snackbar.Add(Localizer[AppStrings.DeleteProductSuccess], Severity.Success);
        NavigatePage("/product-list");
      }
      catch
      {
        Snackbar.Add(Localizer[AppStrings.DeleteProductError], Severity.Error);
      }
    }
  }
}
