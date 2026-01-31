using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;
using BarcodeSalesApp.Application.Features.Helpers;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Queries;

public class GetSalesRecordsByDateQuery : IRequest<GetSalesRecordsByDateResult>
{
  public DateOnly SaleDate { get; set; }
  public int PageSize { get; set; }
  public int PageNumber { get; set; }
  public SalesRecordSortColumn OrderBy { get; set; } = SalesRecordSortColumn.Name;
  public SortDirection Direction { get; set; } = SortDirection.Ascending;
  public string? SearchText { get; set; }
  public GetSalesRecordsByDateQuery(DateOnly saleDate, int pageSize, int pageNumber)
  {
    PageSize = pageSize;
    PageNumber = pageNumber;
    SaleDate = saleDate;
  }
}
public enum SalesRecordSortColumn
{
  Name,
  SalePrice,
  QuantitySold,
  TotalProfit,
}