using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, FinancialDbContext>, ITransactionRepository
{
    public TransactionRepository(FinancialDbContext context) : base(context) { }
    public override void BeforeAdd(Transaction entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Transaction entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}