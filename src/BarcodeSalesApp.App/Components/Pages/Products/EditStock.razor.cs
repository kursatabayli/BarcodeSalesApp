using AutoMapper;
using BarcodeSalesApp.App.Components.Helpers;
using BarcodeSalesApp.App.Models.StockModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using BarcodeSalesApp.Application.Features.CQRS.Stocks.Commands;
using BarcodeSalesApp.Application.Features.CQRS.Stocks.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Products;

public partial class EditStock : ComponentBase
{
  [Parameter] public long Id { get; set; }
  [Inject] public IMediator Mediator { get; set; } = default!;
  [Inject] public IMapper Mapper { get; set; } = default!;
  [Inject] public NavigationManager NavigationManager { get; set; } = default!;
  [Inject] public IDialogService DialogService { get; set; } = default!;
  [Inject] public ISnackbar Snackbar { get; set; } = default!;
  [Inject] public IStringLocalizer<AppStrings> Localizer { get; set; } = default!;

  public StockModel StockModel { get; set; } = new();
  public int NewStockUnit { get; set; }
  private bool isUpdateByBox = true;
  private int originalStockQuantity;
  protected override async Task OnInitializedAsync()
  {
    var result = await Mediator.Send(new GetStockByIdQuery(Id));
    Mapper.Map(result, StockModel);
    originalStockQuantity = StockModel.QuantityInStock;
  }

  public async Task UpdateStockAsync()
  {
    if (isUpdateByBox)
      StockModel.QuantityInStock = originalStockQuantity + ((StockModel.UnitsPerCase ?? 0) * NewStockUnit);
    UpdateStockCommand command = new() { Id = Id, QuantityInStock = StockModel.QuantityInStock };
    var result = await Mediator.Send(command);
    if (result)
    {
      Snackbar.Add(Localizer[AppStrings.UpdateStockSuccess], Severity.Success);
      NavigateTo($"/product-detail/{Id}");
    }
    // Snackbar.Add(Localizer[AppStrings.UpdateStockError], Severity.Error);
    return;
  }

  public void NavigateTo(string url) => NavigationManager.NavigateTo(url);
}
