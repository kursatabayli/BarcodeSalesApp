using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Queries;
using BarcodeSalesApp.Contracts.Repositories;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Handlers;

public class GetEarliestSalesRecordDateQueryHandler : IRequestHandler<GetEarliestSalesRecordDateQuery, DateTime?>
{
  private readonly ISalesRecordRepository _salesRecordRepository;

  public GetEarliestSalesRecordDateQueryHandler(ISalesRecordRepository salesRecordRepository)
  {
    _salesRecordRepository = salesRecordRepository;
  }

  public async Task<DateTime?> Handle(GetEarliestSalesRecordDateQuery request, CancellationToken cancellationToken)
  {
    return await _salesRecordRepository.GetEarliestSalesRecordDateAsync();
  }

}
