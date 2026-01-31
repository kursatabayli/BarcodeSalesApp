using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Commands;

public record UpdateProductCommand(long Id, string Name, decimal PurchasePrice, decimal SalePrice, string? Barcode, int UnitsPerCase) : IRequest<bool>;
