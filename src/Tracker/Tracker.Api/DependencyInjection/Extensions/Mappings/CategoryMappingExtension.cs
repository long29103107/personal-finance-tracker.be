using static Shared.Dtos.Tracker.CategoryDtos;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions.Mappings;

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
            UserId = category.UserId
        };
    }
}
