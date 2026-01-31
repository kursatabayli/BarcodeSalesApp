using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
  {
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
    if (product == null) return false;

    _productRepository.Remove(product);
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return true;
  }
}
