using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class RoleRepository : RepositoryBase<Role, CustomIdentityDbContext>, IRoleRepository
{
    public RoleRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(Role entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Role entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}
