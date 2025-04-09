using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class GoalException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The goal `{id}` was not found.") { }
    }

}
