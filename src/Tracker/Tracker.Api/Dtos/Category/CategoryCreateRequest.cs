using Shared.Domain;
using System.ComponentModel.DataAnnotations;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Category;

public sealed class CategoryCreateRequest : Request
{
    public string Email { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    [AllowedValues(TransactionTypeConstants.Expense, TransactionTypeConstants.Income)]
    public string Type { get; set; }
}
