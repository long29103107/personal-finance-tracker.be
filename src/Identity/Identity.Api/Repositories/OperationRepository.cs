using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Repository;

namespace Identity.Api.Repositories;

public class OperationRepository : RepositoryBase<Operation, CustomIdentityDbContext>, IOperationRepository
{
    public OperationRepository(CustomIdentityDbContext context) : base(context)
    {
    }

    public override void BeforeAdd(Operation entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Operation entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}
