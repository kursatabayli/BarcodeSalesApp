using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Application.Features.Helpers;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Queries;

public class GetAllProductsQuery : IRequest<PagedResult<ProductResult>>
{
  public int PageSize { get; set; }
  public int PageNumber { get; set; }
  public ProductSortColumn OrderBy { get; set; } = ProductSortColumn.Name;
  public SortDirection Direction { get; set; } = SortDirection.Ascending;
  public string? SearchText { get; set; }
  public GetAllProductsQuery(int pageSize, int pageNumber)
  {
    PageSize = pageSize;
    PageNumber = pageNumber;
  }
}


public enum ProductSortColumn
{
  Name,
  SalePrice,
  PurchasePrice,
  Profit,
  StockQuantity
}