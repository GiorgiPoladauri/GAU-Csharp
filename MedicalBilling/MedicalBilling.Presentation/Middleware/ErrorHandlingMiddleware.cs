using System.Net;
using System.Text.Json;

namespace MedicalBilling.Presentation.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleAsync(context, ex);
        }
    }

    private static Task HandleAsync(HttpContext ctx, Exception ex)
    {
        var (status, message) = ex switch
        {
            KeyNotFoundException      => (HttpStatusCode.NotFound, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Forbidden, ex.Message),
            ArgumentException         => (HttpStatusCode.BadRequest, ex.Message),
            InvalidOperationException => (HttpStatusCode.Conflict, ex.Message),
            _                         => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = (int)status;

        return ctx.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            statusCode = (int)status,
            message,
            timestamp = DateTime.UtcNow
        }));
    }
}
