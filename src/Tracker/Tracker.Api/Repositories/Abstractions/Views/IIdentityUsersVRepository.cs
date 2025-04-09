using Shared.Repository.Abstractions;
using Tracker.Api.Entities.Views;

namespace Tracker.Api.Repositories.Abstractions.Views;

public interface IIdentityUsersVRepository : IRepositoryReadonlyBase<IdentityUsersV, FinancialDbContext>
{
}