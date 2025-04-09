﻿using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Transaction;

public sealed class TransactionListResponse : Response
{
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
}
