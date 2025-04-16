using Shared.Domain.Exceptions;

namespace Identity.Api.Exceptions;

public static class RoleException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The role `{id}` was not found.") { }
    }
}
