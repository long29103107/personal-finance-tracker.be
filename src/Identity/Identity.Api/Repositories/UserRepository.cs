using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class UserRepository : RepositoryBase<User, CustomIdentityDbContext>, IUserRepository
{
    public UserRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
