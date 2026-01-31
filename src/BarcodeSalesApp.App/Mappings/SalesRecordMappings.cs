using AutoMapper;
using BarcodeSalesApp.App.Models.SalesRecordModels;
using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Results;

namespace BarcodeSalesApp.App.Mappings;

public class SalesRecordMappings : Profile
{
  public SalesRecordMappings()
  {
    CreateMap<SalesRecordResult, SalesRecordModel>();
  }

}
