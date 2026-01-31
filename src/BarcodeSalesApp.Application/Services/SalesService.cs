using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Commands;
using BarcodeSalesApp.Contracts.Services;
using MediatR;

namespace BarcodeSalesApp.Application.Services;

public class SalesService : ISalesService
{
  private readonly IMediator _mediator;

  public SalesService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<bool> CreateSaleAsync(IList<CartItemHelper> cartItemHelpers)
  {
    var command = new CreateSaleCommand(cartItemHelpers);
    return await _mediator.Send(command);
  }
}
