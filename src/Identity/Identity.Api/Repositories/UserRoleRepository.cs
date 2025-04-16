using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class UserRoleRepository : RepositoryBase<UserRole, CustomIdentityDbContext>, IUserRoleRepository
{
    public UserRoleRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(UserRole entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(UserRole entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}