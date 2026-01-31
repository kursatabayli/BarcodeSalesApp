using BarcodeSalesApp.Application.Features.CQRS.Stocks.Results;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Stocks.Queries;

public class GetStockByIdQuery : IRequest<StockResult?>
{
  public long Id { get; }
  public GetStockByIdQuery(long id)
  {
    Id = id;
  }
}
