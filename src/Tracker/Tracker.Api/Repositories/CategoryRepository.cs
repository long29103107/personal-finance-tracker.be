using Microsoft.EntityFrameworkCore;
using Shared.Repository;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class CategoryRepository : RepositoryBase<Category, FinancialDbContext>, ICategoryRepository
{
    public CategoryRepository(FinancialDbContext context) : base(context) { }

    public override void BeforeAdd(Category entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = string.Empty;
    }

    public override void BeforeUpdate(Category entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = string.Empty;
    }
}