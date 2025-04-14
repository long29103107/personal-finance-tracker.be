using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class ScopeRepository : RepositoryBase<Scope, CustomIdentityDbContext>, IScopeRepository
{
    public ScopeRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
