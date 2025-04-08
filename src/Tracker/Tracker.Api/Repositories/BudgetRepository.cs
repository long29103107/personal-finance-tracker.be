using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class BudgetRepository : RepositoryBase<Budget, FinancialDbContext>, IBudgetRepository
{
    public BudgetRepository(FinancialDbContext context) : base(context) { }
}