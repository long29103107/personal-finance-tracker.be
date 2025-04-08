using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Exceptions;

namespace Shared.ExceptionHandler;


public class CustomProblemDetails : ProblemDetails
{
    public IReadOnlyCollection<ValidationError> Errors { get; set; }
}
