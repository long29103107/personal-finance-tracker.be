using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class CategoryException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The category `{id}` was not found.") { }
    }

    public class HasTransactions : DomainException
    {
        public HasTransactions(int id) : base("Category has transactions", $"The category `{id}` has transactions.") { }
    }
}
