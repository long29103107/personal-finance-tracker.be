using Tracker.Api.Constants;

namespace Tracker.Api.Dtos;

public class CategoryDto
{
    public string Name { get; set; }
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public int? ParentCategoryId { get; set; }
}
