using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Queries;

public class GetProductByBarcodeQuery : IRequest<ProductResult?>
{
  public string Barcode { get; set; }

  public GetProductByBarcodeQuery(string barcode)
  {
    Barcode = barcode;
  }
}
