using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Domain.Entities;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class AddProductHandler : IRequestHandler<AddProductCommand, long>
{
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
  {
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<long> Handle(AddProductCommand request, CancellationToken cancellationToken)
  {
    var product = ProductEntity.Add(
      request.Name,
      request.PurchasePrice,
      request.SalePrice,
      request.Barcode,
      request.UnitsPerCase);

    await _productRepository.AddAsync(product, cancellationToken);
    await _unitOfWork.SaveChangesAsync();

    return product.Id;
  }
}
