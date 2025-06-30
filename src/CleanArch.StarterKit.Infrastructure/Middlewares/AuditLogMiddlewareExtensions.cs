using Microsoft.AspNetCore.Builder;

namespace CleanArch.StarterKit.Infrastructure.Middlewares;
public static class AuditLogMiddlewareExtensions
{
    public static IApplicationBuilder UseAuditLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuditLogMiddleware>();
    }
}
