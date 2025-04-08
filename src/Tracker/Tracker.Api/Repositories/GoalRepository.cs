using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;
public class GoalRepository : RepositoryBase<Goal, FinancialDbContext>, IGoalRepository
{
    public GoalRepository(FinancialDbContext context) : base(context) { }
}