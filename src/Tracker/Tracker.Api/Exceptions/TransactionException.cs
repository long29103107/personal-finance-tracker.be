using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class TransactionException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The transaction `{id}` was not found.") { }
    }

}
