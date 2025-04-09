
using Shared.Repository;
using Tracker.Api.Entities.Views;
using Tracker.Api.Repositories.Abstractions.Views;

namespace Tracker.Api.Repositories.Views;

public class IdentityUsersVRepository : RepositoryReadonlyBase<IdentityUsersV, FinancialDbContext>, IIdentityUsersVRepository
{
    public IdentityUsersVRepository(FinancialDbContext context) : base(context)
    {
    }
}
