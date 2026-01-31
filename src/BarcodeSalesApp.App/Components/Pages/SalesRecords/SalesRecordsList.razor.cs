using AutoMapper;
using BarcodeSalesApp.App.Models.SalesRecordModels;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SortDirection = BarcodeSalesApp.Application.Features.Helpers.SortDirection;

namespace BarcodeSalesApp.App.Components.Pages.SalesRecords;

public partial class SalesRecordsList : ComponentBase
{
  [Inject] private IMediator Mediator { get; set; } = default!;
  [Inject] private IMapper Mapper { get; set; } = default!;
  [Inject] private IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  private decimal TotalSales { get; set; }
  private decimal TotalProfit { get; set; }
  private DateTime? SelectedDate { get; set; } = DateTime.Today;
  private DateTime? MinDate { get; set; }
  private DateOnly SelectedDateOnly
  {
    get => SelectedDate.HasValue ? DateOnly.FromDateTime(SelectedDate.Value) : DateOnly.FromDateTime(DateTime.Today);
  }
  private MudTable<SalesRecordModel> _table = default!;
  private string _searchText = "";
  private int Count = 0;
  private string SearchText
  {
    get => _searchText;
    set
    {
      _searchText = value;
      _table.ReloadServerData();
    }
  }
  protected override async Task OnInitializedAsync()
  {
    MinDate = await Mediator.Send(new GetEarliestSalesRecordDateQuery()) ?? DateTime.Today;
  }

  private async Task<TableData<SalesRecordModel>> ServerReload(TableState state, CancellationToken token = default)
  {
    var orderByColumn = Enum.TryParse<SalesRecordSortColumn>(state.SortLabel, out var sortColumn)
                        ? sortColumn
                        : SalesRecordSortColumn.Name;
    var query = new GetSalesRecordsByDateQuery(SelectedDateOnly, state.PageSize, state.Page + 1)
    {
      SearchText = SearchText,
      OrderBy = orderByColumn,
      Direction = TranslateSortDirection(state.SortDirection)
    };
    var result = await Mediator.Send(query, token);
    var uiModels = Mapper.Map<List<SalesRecordModel>>(result.PagedResult.Items);
    TotalSales = result.GrandTotalSales;
    TotalProfit = result.GrandTotalProfit;
    StateHasChanged();
    return new TableData<SalesRecordModel>()
    {
      Items = uiModels,
      TotalItems = result.PagedResult.TotalCount
    };
  }
  private async Task OnDateChangedAsync() => await _table.ReloadServerData();
  private static SortDirection TranslateSortDirection(MudBlazor.SortDirection mudDirection)
  {
    return mudDirection switch
    {
      MudBlazor.SortDirection.Ascending => SortDirection.Ascending,
      MudBlazor.SortDirection.Descending => SortDirection.Descending,
      _ => SortDirection.Ascending
    };
  }
}