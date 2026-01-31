using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Stocks.Commands;

public class UpdateStockCommand : IRequest<bool>
{
  public long Id { get; set; }
  public int QuantityInStock { get; set; }
}
