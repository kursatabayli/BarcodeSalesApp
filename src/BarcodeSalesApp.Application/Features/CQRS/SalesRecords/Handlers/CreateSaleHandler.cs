using BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Commands;
using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Domain.Entities;
using MediatR;

namespace BarcodeSalesApp.Application.Features.CQRS.SalesRecords.Handlers;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, bool>
{
  private readonly ISalesRecordRepository _salesRecordRepository;
  private readonly IStockRepository _stockRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateSaleHandler(ISalesRecordRepository salesRecordRepository, IStockRepository stockRepository, IUnitOfWork unitOfWork)
  {
    _salesRecordRepository = salesRecordRepository;
    _stockRepository = stockRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<bool> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
  {
    await _unitOfWork.BeginTransactionAsync(cancellationToken);
    try
    {
      var productIds = request.CartItems.Select(item => item.ProductId).ToList();
      var today = DateOnly.FromDateTime(DateTime.Now);

      var stocks = await _stockRepository.GetByProductIdsAsync(productIds, cancellationToken);

      var stockDictionary = stocks.ToDictionary(s => s.ProductId);

      var existingSales = await _salesRecordRepository.GetExistingRecordsAsync(today, productIds, cancellationToken);

      var salesDictionary = existingSales.ToDictionary(sr => sr.ProductId);

      foreach (var item in request.CartItems)
      {
        if (!stockDictionary.TryGetValue(item.ProductId, out var stockEntity))
        {
          throw new Exception($"Product with ID {item.ProductId} not found in stock.");
        }

        stockEntity.RemoveStock(item.Quantity);

        if (salesDictionary.TryGetValue(item.ProductId, out var existingRecord))
        {
          existingRecord.IncreaseQuantity(item.Quantity);
        }
        else
        {
          var salesRecord = SalesRecordEntity.Add(stockEntity.Product, item.Quantity);
          await _salesRecordRepository.AddAsync(salesRecord, cancellationToken);
        }
      }

      await _unitOfWork.SaveChangesAsync(cancellationToken);
      await _unitOfWork.CommitTransactionAsync(cancellationToken);
      return true;

    }
    catch
    {
      await _unitOfWork.RollbackTransactionAsync(cancellationToken);
      return false;
    }

  }
}
