using Shared.Domain;
using System.ComponentModel.DataAnnotations;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Category;

public sealed class CategoryCreateRequest : Request
{
    public int? UserId { get; set; }
    public int? ParentCategoryId { get; set; }
    public required string Name { get; set; }
    [AllowedValues(TransactionTypeConstants.Expense, TransactionTypeConstants.Income)]
    public string Type { get; set; }
}
