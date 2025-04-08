using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class AccountException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The account `{id}` was not found.") { }
    }
}
