using Shared.Domain.Exceptions;

namespace Identity.Api.Exceptions;

public static class UserException
{
    public class NotFoundUserEmail : NotFoundException
    {
        public NotFoundUserEmail(string email) : base($"The user `{email}` was not found.") { }
    }

    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The user `{id}` was not found.") { }
    }

    public class Conflict : ConflictException
    {
        public Conflict(string message) : base(message) { }
    }
}
