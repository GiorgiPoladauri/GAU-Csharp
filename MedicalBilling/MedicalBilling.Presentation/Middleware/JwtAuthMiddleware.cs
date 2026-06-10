namespace MedicalBilling.Presentation.Middleware;

public class JwtAuthMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
    }
}

public static class JwtAuthMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtAuth(this IApplicationBuilder app)
        => app.UseAuthentication().UseAuthorization();
}