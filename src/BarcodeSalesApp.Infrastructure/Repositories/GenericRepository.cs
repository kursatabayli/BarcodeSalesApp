using System.Linq.Expressions;
using BarcodeSalesApp.Contracts.Repositories;
using BarcodeSalesApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarcodeSalesApp.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
  protected readonly AppDbContext _context;
  protected readonly DbSet<TEntity> _dbSet;

  public GenericRepository(AppDbContext context)
  {
    _context = context;
    _dbSet = _context.Set<TEntity>();
  }

  public IQueryable<TEntity> GetAll() => _dbSet.AsNoTracking();
  public async ValueTask<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken) => await _dbSet.FindAsync([id], cancellationToken);
  public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) => await _dbSet.AddAsync(entity, cancellationToken);
  public void Update(TEntity entity) => _dbSet.Update(entity);
  public void Remove(TEntity entity) => _dbSet.Remove(entity);
  public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);
  public IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
  public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => await _dbSet.AnyAsync(predicate, cancellationToken);
}
