using BarcodeSalesApp.Application.Features.CQRS.Stocks.Queries;
using BarcodeSalesApp.Application.Features.CQRS.Stocks.Results;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Stocks.Handlers;

public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, StockResult?>
{
  private readonly IStockRepository _stockRepository;
  public GetStockByIdQueryHandler(IStockRepository stockRepository)
  {
    _stockRepository = stockRepository;
  }

  public async Task<StockResult?> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
  {
    var stock = await _stockRepository.GetByIdAsync(request.Id, cancellationToken);
    if (stock == null)
      return null;

    return new StockResult
    {
      ProductId = stock.ProductId,
      Name = stock.Product.Name,
      UnitsPerCase = stock.Product.UnitsPerCase,
      QuantityInStock = stock.QuantityInStock
    };
  }
}
