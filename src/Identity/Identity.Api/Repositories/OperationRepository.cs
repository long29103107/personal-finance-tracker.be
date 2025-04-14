using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class OperationRepository : RepositoryBase<Operation, CustomIdentityDbContext>, IOperationRepository
{
    public OperationRepository(CustomIdentityDbContext context) : base(context)
    {
    }
}
