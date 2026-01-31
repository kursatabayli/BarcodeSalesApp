using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
  {
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
    if (product == null) return false;

    product.Update(
        request.Name,
        request.PurchasePrice,
        request.SalePrice,
        request.Barcode,
        request.UnitsPerCase
    );
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    return true;
  }
}
