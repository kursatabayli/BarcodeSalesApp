using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Queries;

public class GetProductByIdQuery : IRequest<ProductResult>
{
  public long Id { get; set; }

  public GetProductByIdQuery(long id)
  {
    Id = id;
  }
}
