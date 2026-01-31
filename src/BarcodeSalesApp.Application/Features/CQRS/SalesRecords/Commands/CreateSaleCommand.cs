using BarcodeSalesApp.Contracts.Services;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Commands;

public class CreateSaleCommand : IRequest<bool>
{
  public IList<CartItemHelper> CartItems { get; set; }
  public CreateSaleCommand(IList<CartItemHelper> cartItems)
  {
    CartItems = cartItems;
  }
}
