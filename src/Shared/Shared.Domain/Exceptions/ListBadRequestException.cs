namespace Shared.Domain.Exceptions;
public sealed class ListBadRequestException : BadRequestException
{
    public ListBadRequestException(string message) : base(message)
    {
    }
}