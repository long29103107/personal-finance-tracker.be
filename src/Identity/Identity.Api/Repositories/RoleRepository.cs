using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class RoleRepository : RepositoryBase<Role, CustomIdentityDbContext>, IRoleRepository
{
    public RoleRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
