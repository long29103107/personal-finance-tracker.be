using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;
public class GoalRepository : RepositoryBase<Goal, FinancialDbContext>, IGoalRepository
{
    public GoalRepository(FinancialDbContext context) : base(context) { }
    public override void BeforeAdd(Goal entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Goal entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}