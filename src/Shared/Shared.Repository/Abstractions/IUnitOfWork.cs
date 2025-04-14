using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Shared.Repository.Abstractions;

public interface IUnitOfWork<TContext> : IDisposable
    where TContext : DbContext
{
    #region Transaction
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion
}
