using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class AccountRepository : RepositoryBase<Account, FinancialDbContext>, IAccountRepository
{
    public AccountRepository(FinancialDbContext context) : base(context) { }
    
    public override void BeforeAdd(Account entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Account entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}