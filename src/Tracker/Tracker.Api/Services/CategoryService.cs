using Tracker.Api.Dtos;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category> CreateCategoryAsync(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Type = dto.Type,
            ParentCategoryId = dto.ParentCategoryId
        };

        return await _repository.CreateAsync(category);
    }

    public async Task<List<Category>> GetAllCategoriesAsync() => await  _repository.GetAllAsync();

    public async Task<Category?> GetCategoryByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<List<Category>> GetSubCategoriesAsync(int parentId) => await _repository.GetByParentIdAsync(parentId);
}