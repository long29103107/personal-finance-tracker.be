using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class BudgetRepository : RepositoryBase<Budget, FinancialDbContext>, IBudgetRepository
{
    public BudgetRepository(FinancialDbContext context) : base(context) { }
    public override void BeforeAdd(Budget entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Budget entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}