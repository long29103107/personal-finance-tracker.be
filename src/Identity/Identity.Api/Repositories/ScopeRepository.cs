using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class ScopeRepository : RepositoryBase<Scope, CustomIdentityDbContext>, IScopeRepository
{
    public ScopeRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(Scope entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Scope entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}
