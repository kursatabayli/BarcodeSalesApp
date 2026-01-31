using AutoMapper;
using BarcodeSalesApp.App.Models.ProductModels;
using BarcodeSalesApp.App.Models.StockModels;
using BarcodeSalesApp.Application.Features.CQRS.Products.Commands;
using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Application.Features.CQRS.Stocks.Results;

namespace BarcodeSalesApp.App.Mappings;

public class ProductMappings : Profile
{
  public ProductMappings()
  {
    CreateMap<ProductResult, ProductModel>();
    CreateMap<AddProductModel, AddProductCommand>();
    CreateMap<ProductModel, UpdateProductCommand>();
    CreateMap<StockResult, StockModel>();
  }
}
