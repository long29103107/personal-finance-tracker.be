using Tracker.Api.Dtos;
using Tracker.Api.Entities;

namespace Tracker.Api.Services.Abstractions;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(CategoryDto dto);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetSubCategoriesAsync(int parentId);
}
