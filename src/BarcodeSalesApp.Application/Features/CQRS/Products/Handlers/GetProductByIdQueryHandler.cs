using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.Products.Queries;
using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResult>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<ProductResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
  {
    var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
    return _mapper.Map<ProductResult>(product);
  }
}
