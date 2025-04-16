using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class UserRepository : RepositoryBase<User, CustomIdentityDbContext>, IUserRepository
{
    public UserRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(User entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(User entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}
