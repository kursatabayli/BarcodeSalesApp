using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Queries;
using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;
using BarcodeSalesApp.Application.Features.Helpers;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Handlers;

public class GetSalesRecordsByDateQueryHandler : IRequestHandler<GetSalesRecordsByDateQuery, GetSalesRecordsByDateResult>
{
  private readonly ISalesRecordRepository _salesRecordRepository;
  private readonly IMapper _mapper;
  public GetSalesRecordsByDateQueryHandler(ISalesRecordRepository salesRecordRepository, IMapper mapper)
  {
    _salesRecordRepository = salesRecordRepository;
    _mapper = mapper;
  }

  public async Task<GetSalesRecordsByDateResult> Handle(GetSalesRecordsByDateQuery request, CancellationToken cancellationToken)
  {
    var baseQuery = _salesRecordRepository.GetAllRecordsByDate(request.SaleDate);

    var grandTotalSales = await baseQuery.SumAsync(p => p.TotalSales, cancellationToken);
    var grandTotalProfit = await baseQuery.SumAsync(p => p.TotalProfit, cancellationToken);
    var filteredQuery = baseQuery;
    if (!string.IsNullOrWhiteSpace(request.SearchText))
    {
      var searchTextLower = request.SearchText.ToLower();
      filteredQuery = filteredQuery.Where(p => p.Product != null && (p.Product.Name.ToLower().Contains(searchTextLower) || p.Product.Barcode != null && p.Product.Barcode.Contains(request.SearchText)));
    }
    var totalCount = await filteredQuery.CountAsync(cancellationToken);

    filteredQuery = request.OrderBy switch
    {
      SalesRecordSortColumn.Name => (request.Direction == SortDirection.Ascending)
                          ? filteredQuery.OrderBy(p => p.Product.Name)
                          : filteredQuery.OrderByDescending(p => p.Product.Name),
      SalesRecordSortColumn.SalePrice => (request.Direction == SortDirection.Ascending)
                          ? filteredQuery.OrderBy(p => p.SalePrice)
                          : filteredQuery.OrderByDescending(p => p.SalePrice),
      SalesRecordSortColumn.QuantitySold => (request.Direction == SortDirection.Ascending)
                          ? filteredQuery.OrderBy(p => p.QuantitySold)
                          : filteredQuery.OrderByDescending(p => p.QuantitySold),
      SalesRecordSortColumn.TotalProfit => (request.Direction == SortDirection.Ascending)
                          ? filteredQuery.OrderBy(p => p.TotalProfit)
                          : filteredQuery.OrderByDescending(p => p.TotalProfit),
      _ => filteredQuery.OrderBy(p => p.Product.Name),
    };

    filteredQuery = filteredQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

    var items = await _mapper.ProjectTo<SalesRecordResult>(filteredQuery).ToListAsync(cancellationToken);

    var pagedResult = new PagedResult<SalesRecordResult>
    {
      Items = items,
      TotalCount = totalCount
    };

    return new GetSalesRecordsByDateResult
    {
      PagedResult = pagedResult,
      GrandTotalSales = grandTotalSales,
      GrandTotalProfit = grandTotalProfit
    };
  }
}
