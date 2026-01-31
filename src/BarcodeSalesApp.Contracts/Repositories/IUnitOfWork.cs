
namespace BarcodeSalesApp.Contracts.Repositories;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
  Task SaveChangesAsync(CancellationToken cancellationToken = default);
  Task BeginTransactionAsync(CancellationToken cancellationToken = default);
  Task CommitTransactionAsync(CancellationToken cancellationToken = default);
  Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
