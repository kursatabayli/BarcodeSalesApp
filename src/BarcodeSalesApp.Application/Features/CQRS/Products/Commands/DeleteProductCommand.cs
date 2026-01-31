using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Commands;

public record DeleteProductCommand(long Id) : IRequest<bool>;
