using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Infrastructure.Context;

namespace BarcodeSalesApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _context;
  private bool _disposed = false;

  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }

  public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
  public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);
  public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.CommitTransactionAsync(cancellationToken);
  public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.RollbackTransactionAsync(cancellationToken);

  public async ValueTask DisposeAsync() => await DisposeAsyncCore();

  protected virtual async ValueTask DisposeAsyncCore()
  {
    if (!_disposed)
    {
      await _context.DisposeAsync();
      _disposed = true;
    }
  }

  public void Dispose()
  {
    if (!_disposed)
    {
      _context.Dispose();
      _disposed = true;
    }
  }
}
