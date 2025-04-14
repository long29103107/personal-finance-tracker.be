using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;
using Tracker.Api.Exceptions;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;
using FluentValidation;
using Shared.Domain.Exceptions;
using ValidationException = Shared.Domain.Exceptions.ValidationException;
using Tracker.Api.Repositories.Abstractions.Views;
using Tracker.Api.DependencyInjection.Extensions.Mappings;
using Shared.Dtos.Abstractions;
using static Shared.Dtos.Tracker.CategoryDtos;


namespace Tracker.Api.Services;

public class CategoryService(ICategoryRepository _cateRepo
    , IValidatorFactory _validatorFactory
    , IIdentityUsersVRepository _identityUsersRepo
    , ITransactionRepository _transactionRepo
    , IScopedCache _scopedCache) 
    
    : ICategoryService
{

    public async Task<IEnumerable<CategoryResponse>> GetListAsync()
    {
        var a = _scopedCache;
        var result = await _cateRepo.FindAll()
            .Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                ParentCategoryId = x.ParentCategoryId,
                UserId = x.UserId
            })
            .ToListAsync();

        return result;
    }

    public async Task<CategoryResponse> GetByIdAsync(int id)
    {
        Category result = await _GetCategoryAsync(id)
           ?? throw new CategoryException.NotFound(id);

        return result.ToCategoryResponse();
    }

    public async Task<List<CategoryResponse>> GetSubCategoriesAsync(int parentId)
    {
        var result = await _cateRepo.FindByCondition(x => x.ParentCategoryId == parentId)
             .Select(x => new CategoryResponse
             {
                 Id = x.Id,
                 Name = x.Name,
                 Type = x.Type,
                 ParentCategoryId = x.ParentCategoryId,
                 UserId = x.UserId
             })
            .ToListAsync();

        return result;
    }

    public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
    {
        var entity = new Category
        {
            Name = request.Name,
            Type = request.Type,
            ParentCategoryId = request.ParentCategoryId,
            UserId = request.UserId
        };

        await _ValidateCategoryAsync(entity);
        _cateRepo.Add(entity);
        await _cateRepo.SaveAsync();

        return entity.ToCategoryResponse();
    }

    public async Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest request)
    {
        Category entity = await _GetCategoryAsync(id)
          ?? throw new CategoryException.NotFound(id);

        entity.UserId = request.UserId;
        entity.ParentCategoryId = request.ParentCategoryId;
        entity.Name = request.Name;
        entity.Type = request.Type;

        await _ValidateCategoryAsync(entity);
        _cateRepo.Update(entity);
        await _cateRepo.SaveAsync();

        return entity.ToCategoryResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Category entity = await _GetCategoryAsync(id)
          ?? throw new CategoryException.NotFound(id);

        await _CheckCategoryHasTransactionsAsync(entity);

        try
        {
            _cateRepo.Remove(entity);
            await _cateRepo.SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task _CheckCategoryHasTransactionsAsync(Category entity)
    {
        var categoryIds = await _cateRepo.FindByCondition(x => x.Id == entity.Id || x.ParentCategoryId == entity.Id)
            .Select(x => x.Id)
            .ToListAsync();

        var hasTransactions = await _transactionRepo.AnyAsync(x => categoryIds.Contains(x.CategoryId));

        if (hasTransactions)
        {
            throw new CategoryException.HasTransactions(entity.Id);
        }

        
    }

    private async Task<Category?> _GetCategoryAsync(int id)
    {
        var result = await _cateRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private async Task _ValidateCategoryAsync(Category model)
    {
        var validator = _validatorFactory.GetValidator<Category>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }
}