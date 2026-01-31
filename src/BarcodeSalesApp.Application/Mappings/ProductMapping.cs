using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.Products.Results;
using BarcodeSalesApp.Domain.Entities;

namespace BarcodeSalesApp.Application.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductEntity, ProductResult>()
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.QuantityInStock : (int?)null));
    }
}
