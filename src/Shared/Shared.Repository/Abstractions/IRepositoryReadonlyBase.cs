using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using System.Linq.Expressions;

namespace Shared.Repository.Abstractions;
public interface IRepositoryReadonlyBase<T, TContext>
    where T : class
    where TContext : DbContext
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false);
    #endregion

    #region Linq 
    bool Any(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    bool Any();
    Task<bool> AnyAsync();
    void Dispose(); 
    #endregion
}
