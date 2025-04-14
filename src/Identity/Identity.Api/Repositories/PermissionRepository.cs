using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class PermissionRepository : RepositoryBase<Permission, CustomIdentityDbContext>, IPermissionRepository
{
    public PermissionRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
