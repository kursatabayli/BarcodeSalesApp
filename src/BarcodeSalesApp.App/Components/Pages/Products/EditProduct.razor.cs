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

public partial class EditProduct : ComponentBase
{
  [Parameter] public long Id { get; set; }
  [Inject] public IMediator Mediator { get; set; } = default!;
  [Inject] public IMapper Mapper { get; set; } = default!;
  [Inject] public ISnackbar Snackbar { get; set; } = default!;
  [Inject] public IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  [Inject] public NavigationManager NavigationManager { get; set; } = default!;
  public ProductModel productModel { get; set; } = new();

  protected override async Task OnInitializedAsync()
  {
    var productResult = await Mediator.Send(new GetProductByIdQuery(Id));
    Mapper.Map(productResult, productModel);
  }

  public async Task UpdateProductAsync()
  {
    if (productModel != null)
    {
      var mappedProduct = Mapper.Map<UpdateProductCommand>(productModel);
      await Mediator.Send(mappedProduct);
      Snackbar.Add(Localizer[AppStrings.ProductUpdated], Severity.Success);
      NavigatePage($"/product-detail/{Id}");
    }
  }

  public void NavigatePage(string url) => NavigationManager.NavigateTo(url);

}
