using Tracker.Api.Dtos.Budget;
using Tracker.Api.Dtos.Category;
using Tracker.Api.Dtos.Transaction;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions.Mapping;

public static class TransactionMappingExtension
{
    public static TransactionResponse ToTransactionResponse(this Transaction transaction)
    {
        return new TransactionResponse()
        {
            Id = transaction.Id,
            AccountId = transaction.AccountId,
            CategoryId = transaction.CategoryId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Date = transaction.Date,
            Description = transaction.Description
        };
    }
}
