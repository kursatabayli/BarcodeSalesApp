using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.Products.Commands;

public record AddProductCommand(string Name, decimal PurchasePrice, decimal SalePrice, string? Barcode, int UnitsPerCase) : IRequest<long>;
