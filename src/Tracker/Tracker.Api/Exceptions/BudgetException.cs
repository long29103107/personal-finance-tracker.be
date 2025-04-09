using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class BudgetException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The budget `{id}` was not found.") { }
    }

}
