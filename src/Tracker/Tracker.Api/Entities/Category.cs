using Shared.Domain.Abstractions;
using Tracker.Api.Constants;

namespace Tracker.Api.Entities;

public class Category : BaseEntity
{
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public List<Category> SubCategories { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}
