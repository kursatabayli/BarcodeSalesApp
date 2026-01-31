using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Application.Features.Helpers;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<PagedResult<ProductResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
  {
    var query = _productRepository.GetAll();
    if (!string.IsNullOrWhiteSpace(request.SearchText))
    {
      var searchTextLower = request.SearchText.ToLower();
      query = query.Where(p => (p.Name != null && p.Name.ToLower().Contains(searchTextLower)) || (p.Barcode != null && p.Barcode.Contains(request.SearchText)));
    }

    var totalCount = await query.CountAsync(cancellationToken);

    query = request.OrderBy switch
    {
      ProductSortColumn.Name => (request.Direction == SortDirection.Ascending)
                          ? query.OrderBy(p => p.Name)
                          : query.OrderByDescending(p => p.Name),
      ProductSortColumn.SalePrice => (request.Direction == SortDirection.Ascending)
                          ? query.OrderBy(p => p.SalePrice)
                          : query.OrderByDescending(p => p.SalePrice),
      ProductSortColumn.PurchasePrice => (request.Direction == SortDirection.Ascending)
                          ? query.OrderBy(p => p.PurchasePrice)
                          : query.OrderByDescending(p => p.PurchasePrice),
      ProductSortColumn.Profit => (request.Direction == SortDirection.Ascending)
                          ? query.OrderBy(p => p.SalePrice - p.PurchasePrice)
                          : query.OrderByDescending(p => p.SalePrice - p.PurchasePrice),
      _ => query.OrderBy(p => p.Name),
    };

    query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

    var items = await _mapper.ProjectTo<ProductResult>(query).ToListAsync(cancellationToken);

    return new PagedResult<ProductResult>
    {
      Items = items,
      TotalCount = totalCount
    };
  }
}
