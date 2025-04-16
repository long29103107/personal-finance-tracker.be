using Shared.Domain.Exceptions;

namespace Identity.Api.Exceptions;

public static class PermissionException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The permission `{id}` was not found.") { }
    }
}
