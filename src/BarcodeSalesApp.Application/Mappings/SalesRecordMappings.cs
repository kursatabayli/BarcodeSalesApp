using AutoMapper;
using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;
using BarcodeSalesApp.Domain.Entities;

namespace BarcodeSalesApp.Application.Mappings;

public class SalesRecordMappings : Profile
{
  public SalesRecordMappings()
  {
    CreateMap<SalesRecordEntity, SalesRecordResult>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Product.Barcode));
  }
}
