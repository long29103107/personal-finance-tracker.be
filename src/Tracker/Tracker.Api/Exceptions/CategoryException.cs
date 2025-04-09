using Shared.Domain.Exceptions;

namespace Tracker.Api.Exceptions;

public static class CategoryException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The category `{id}` was not found.") { }
    }
}
