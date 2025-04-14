using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class UserRoleRepository : RepositoryBase<UserRole, CustomIdentityDbContext>, IUserRoleRepository
{
    public UserRoleRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}