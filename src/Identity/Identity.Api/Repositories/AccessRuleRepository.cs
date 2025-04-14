using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class AccessRuleRepository : RepositoryBase<AccessRule, CustomIdentityDbContext>, IAccessRuleRepository
{
    public AccessRuleRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
