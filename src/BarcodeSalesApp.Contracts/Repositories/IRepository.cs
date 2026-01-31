using System.Linq.Expressions;

namespace BarcodeSalesApp.Contracts.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    ValueTask<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}
