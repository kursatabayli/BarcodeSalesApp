using AutoMapper;
using BarcodeSalesApp.App.Models.ProductModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SortDirection = BarcodeSalesApp.Application.Features.Helpers.SortDirection;

namespace BarcodeSalesApp.App.Components.Pages.Products;

public partial class ProductList : ComponentBase
{
  [Inject] public IMediator Mediator { get; set; } = default!;
  [Inject] public IMapper Mapper { get; set; } = default!;
  [Inject] public NavigationManager NavigationManager { get; set; } = default!;
  [Inject] public IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  private MudTable<ProductModel> _table = default!;
  private string _searchText = "";
  private int Count = 0;
  public string SearchText
  {
    get => _searchText;
    set
    {
      _searchText = value;
      _table.ReloadServerData();
    }
  }

  private async Task<TableData<ProductModel>> ServerReload(TableState state, CancellationToken token = default)
  {
    var orderByColumn = Enum.TryParse<ProductSortColumn>(state.SortLabel, out var sortColumn)
                            ? sortColumn
                            : ProductSortColumn.Name;

    var query = new GetAllProductsQuery(state.PageSize, state.Page + 1)
    {
      SearchText = SearchText,
      OrderBy = orderByColumn,
      Direction = TranslateSortDirection(state.SortDirection)
    };

    var pagedResult = await Mediator.Send(query, token);

    var uiModels = Mapper.Map<IList<ProductModel>>(pagedResult.Items);

    return new TableData<ProductModel>()
    {
      Items = uiModels,
      TotalItems = pagedResult.TotalCount
    };
  }

  private static SortDirection TranslateSortDirection(MudBlazor.SortDirection mudDirection)
  {
    return mudDirection switch
    {
      MudBlazor.SortDirection.Ascending => SortDirection.Ascending,
      MudBlazor.SortDirection.Descending => SortDirection.Descending,
      _ => SortDirection.Ascending
    };
  }
  public void ProductDetail(ProductModel product) => NavigationManager.NavigateTo($"/product-detail/{product.Id}");
  public void AddProduct() => NavigationManager.NavigateTo("/add-product");
}
