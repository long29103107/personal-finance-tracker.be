using Tracker.Api.Entities;

namespace Tracker.Api.Repositories.Abstractions;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetByParentIdAsync(int parentId);
}
