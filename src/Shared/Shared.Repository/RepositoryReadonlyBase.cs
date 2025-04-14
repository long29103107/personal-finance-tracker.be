using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Extensions;
using Shared.Repository.Abstractions;
using System.Linq.Expressions;

namespace Shared.Repository;

public abstract class RepositoryReadonlyBase<T, TContext> : IRepositoryReadonlyBase<T, TContext>
    where T : class
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly bool _needSetAuditLog = true;

    public RepositoryReadonlyBase(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    #region Filter
    public virtual IQueryable<T> Filter()
    {
        return _context.Set<T>();
    }
    #endregion

    #region Query
    public IQueryable<T> FindAll(bool isTracking = false)
    {
        if (isTracking)
        {
            return Filter();
        }

        return Filter().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return Filter().Where(expression);
        }

        return Filter().AsNoTracking().Where(expression);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return await FindByCondition(expression, isTracking).FirstOrDefaultAsync();
    }

    public T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return FindByCondition(expression, isTracking).FirstOrDefault();
    }

    public IQueryable<T> Include(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Include(expression);
    }
    public IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions)
    {
        if (expressions.IsNullOrEmpty())
            return _context.Set<T>();

        foreach (var expression in expressions)
        {
            _context.Set<T>().Include(expression);
        }

        return _context.Set<T>();
    }

    #endregion

    #region Linq 
    public bool Any()
    {
        return Filter().Any();
    }
    
    public async Task<bool> AnyAsync()
    {
        return await Filter().AnyAsync();
    }

    public bool Any(Expression<Func<T, bool>> expression)
    {
        return Filter().Where(expression).Any();
    }
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await Filter().Where(expression).AnyAsync();
    }

    public void Dispose()
    {
        _context.Dispose();

    }
    #endregion
}

