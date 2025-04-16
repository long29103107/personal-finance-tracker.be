using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Abstractions;

namespace Shared.Repository;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    protected readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    #region Transaction
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await SaveAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public async Task TruncateAsync(string tableName)
    {
        await _context.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}");
    }

    public void DetachEntities()
    {
        _context.ChangeTracker.Clear();
    }
    #endregion
}
