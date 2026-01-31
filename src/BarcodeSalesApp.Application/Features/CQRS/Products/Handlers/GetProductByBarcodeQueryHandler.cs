using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class GetProductByBarcodeQueryHandler : IRequestHandler<GetProductByBarcodeQuery, ProductResult?>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetProductByBarcodeQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<ProductResult?> Handle(GetProductByBarcodeQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByBarcodeAsync(request.Barcode, cancellationToken);
    if (product == null)
      return null;

    return _mapper.Map<ProductResult>(product);
  }
}
