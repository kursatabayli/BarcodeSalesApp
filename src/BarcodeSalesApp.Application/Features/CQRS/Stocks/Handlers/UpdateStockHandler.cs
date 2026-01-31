using BarcodeSalesApp.Application.Features.CQRS.Stocks.Commands;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Stocks.Handlers;

public class UpdateStockHandler : IRequestHandler<UpdateStockCommand, bool>
{
  private readonly IStockRepository _stockRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateStockHandler(IStockRepository stockRepository, IUnitOfWork unitOfWork)
  {
    _stockRepository = stockRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<bool> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
  {
    var stock = await _stockRepository.GetByIdAsync(request.Id, cancellationToken);
    stock.UpdateStock(request.QuantityInStock);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return true;
  }
}
