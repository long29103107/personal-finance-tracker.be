using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using Shared.Repository.Abstractions;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using Shared.Lib;

namespace Shared.Repository;

public abstract class RepositoryBase<T, TContext> : IRepositoryBase<T, TContext>
    where T : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly bool _needSetAuditLog = true;

    public RepositoryBase(TContext context)
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

    #region Action
    public void Add(T entity)
    {
        if(_needSetAuditLog)
        {
            BeforeAdd(entity);
        }
        _context.Set<T>().Add(entity);
    }

    public void AddRange(List<T> entities)
    {
        if(entities.IsNullOrEmpty())
            return;

        foreach (var entity in entities)
        {
            if (_needSetAuditLog)
            {
                BeforeAdd(entity);
            }
        }

        _context.Set<T>().AddRange(entities);
    }

    public void Update(T entity)
    {
        if(_needSetAuditLog)
        {
            BeforeUpdate(entity);
        }
        _context.Set<T>().Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        if (entities.IsNullOrEmpty())
            return;

        foreach (var entity in entities)
        {
            if (_needSetAuditLog)
            {
                BeforeUpdate(entity);
            }
        } 
        _context.Set<T>().UpdateRange(entities);
    }

    public void Remove(T entity)
    {
        if (_needSetAuditLog)
        {
            BeforeRemove(entity);
        }
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(List<T> entities)
    {
        if (entities.IsNullOrEmpty())
            return;

        foreach (var entity in entities)
        {
            if (_needSetAuditLog)
            {
                BeforeRemove(entity);
            }
        }
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void BeforeAdd(T entity)
    {

    }

    public virtual void BeforeUpdate(T entity)
    {

    }

    public virtual void BeforeRemove(T entity)
    {

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

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();

    }
    public int Save()
    {
        return _context.SaveChanges();

    }

    public void Dispose()
    {
        _context.Dispose();

    }
    #endregion
}

