using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Queries;

public class GetEarliestSalesRecordDateQuery : IRequest<DateTime?>
{
}
