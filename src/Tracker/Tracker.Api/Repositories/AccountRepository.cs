using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class AccountRepository : RepositoryBase<Account, FinancialDbContext>, IAccountRepository
{
    public AccountRepository(FinancialDbContext context) : base(context) { }
}