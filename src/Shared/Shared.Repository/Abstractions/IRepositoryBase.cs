using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using System.Linq.Expressions;

namespace Shared.Repository.Abstractions;
public interface IRepositoryBase<T, TContext>
    where T : class
    where TContext : DbContext
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false);
    IQueryable<T> Include(Expression<Func<T, bool>> expression);
    IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions);
    #endregion

    #region Action
    void Add(T entity);
    void AddRange(List<T> entities);
    void Update(T entity);
    void UpdateRange(List<T> entities);
    void Remove(T entity);
    void RemoveRange(List<T> entities);
    void BeforeAdd(T entity);
    void BeforeUpdate(T entity);
    void BeforeRemove(T entity);
    #endregion

    #region Linq 
    bool Any(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    bool Any();
    Task<bool> AnyAsync();
    Task<int> SaveAsync();
    int Save();
    void Dispose();
    void SetAuditLog(bool needToAuditLog);
    #endregion
}
