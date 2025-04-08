namespace Shared.Domain.Exceptions;
public class ErrorException : DomainException
{
    public ErrorException(string message)
        : base("Error", message)
    {
    }
}
