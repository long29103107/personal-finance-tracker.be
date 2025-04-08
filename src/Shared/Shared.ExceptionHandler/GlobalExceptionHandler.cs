using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Shared.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using ILogger = Serilog.ILogger;
using ValidationException = Shared.Domain.Exceptions.ValidationException;

namespace Shared.ExceptionHandler;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public GlobalExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.Error(exception, exception.Message);

        var statusCode = _GetStatusCode(exception);

        var problemDetails = new CustomProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(exception),
            Detail = exception.Message,
            Errors = GetErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static int _GetStatusCode(Exception exception)
    {
        var result = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            ConflictException => StatusCodes.Status409Conflict,
            ServiceUnavailableException => StatusCodes.Status503ServiceUnavailable,
            ErrorException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return result;
    }

    private static string GetTitle(Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            return validationException.Title;
        }

        var result = exception switch
        {
            DomainException applicationException => applicationException.Title,
            _ => "Server Error"
        };

        return result;
    }

    private static IReadOnlyCollection<ValidationError> GetErrors(Exception exception)
    {
        IReadOnlyCollection<ValidationError> errors = default;

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }
}