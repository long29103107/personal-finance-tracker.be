using Tracker.Api.Dtos.Category;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions;

public static class CategoryMappingExtension
{
    public static CategoryResponse ToCategoryResponse(this Category category)
    {
        return new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            ParentCategoryId = category.ParentCategoryId,
            Email = category.Email
        };
    }
}
