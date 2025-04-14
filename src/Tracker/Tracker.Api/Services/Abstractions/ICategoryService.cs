using static Shared.Dtos.Tracker.CategoryDtos;

namespace Tracker.Api.Services.Abstractions;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetListAsync();
    Task<CategoryResponse> GetByIdAsync(int id);
    Task<List<CategoryResponse>> GetSubCategoriesAsync(int parentId);
    Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
    Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
