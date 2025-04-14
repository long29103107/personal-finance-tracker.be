using Tracker.Api.Entities;
using static Shared.Dtos.Tracker.TransactionDtos;

namespace Tracker.Api.DependencyInjection.Extensions.Mappings;

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
