using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class AccessRuleRepository : RepositoryBase<AccessRule, CustomIdentityDbContext>, IAccessRuleRepository
{
    public AccessRuleRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(AccessRule entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(AccessRule entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}
