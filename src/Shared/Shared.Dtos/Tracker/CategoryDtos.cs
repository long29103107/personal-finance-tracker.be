using Shared.Dtos.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Tracker;

public static class CategoryDtos
{
    private const string _expense = "Expense";
    private const string _income = "Income";
    //Request
    public sealed class CategoryCreateRequest : Request
    {
        public int? UserId { get; set; }
        public int? ParentCategoryId { get; set; }
        public required string Name { get; set; }
        [AllowedValues(_expense, _income)]
        public string Type { get; set; }
    }

    public sealed class CategoryUpdateRequest : Request
    {
        public int? UserId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = _expense;
    }

    //Response
    public sealed class CategoryListResponse : Response
    {
        public string Email { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = _expense;
    }

    public sealed class CategoryResponse : Response
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = _expense;
    }

}
