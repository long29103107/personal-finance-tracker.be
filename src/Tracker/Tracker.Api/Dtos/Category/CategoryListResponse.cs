﻿using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Category;

public sealed class CategoryListResponse : Response
{
    public string Email { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = TransactionTypeConstants.Expense;
}
