using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Platform.Domain.Common.Exceptions;

namespace Platform.Api.ExceptionHandlers;

internal sealed class NotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger<NotFoundExceptionHandler> _logger;

    public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DomainException { Kind: DomainExceptionKind.NotFound } domainException)
        {
            return false;
        }

        _logger.LogError(exception, "Exception occurred");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not Found",
            Detail = domainException.Description,
            Type = "http://www.rfc-editor.org/info/rfc6838",
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = new Dictionary<string, object?>
            {
                {"errors", new[] {domainException.Code, domainException.Description}}
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
