using Microsoft.AspNetCore.Builder;

namespace CleanArch.StarterKit.Infrastructure.Middlewares;

/// <summary>
/// Extension methods for registering the audit logging middleware.
/// </summary>
public static class AuditLogMiddlewareExtensions
{
    /// <summary>
    /// Adds the <see cref="AuditLogMiddleware"/> to the application's request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The updated application builder.</returns>
    public static IApplicationBuilder UseAuditLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuditLogMiddleware>();
    }
}
