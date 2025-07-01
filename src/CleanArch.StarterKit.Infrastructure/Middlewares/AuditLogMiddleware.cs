using CleanArch.StarterKit.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArch.StarterKit.Infrastructure.Middlewares;

/// <summary>
/// Middleware that logs incoming HTTP requests for auditing purposes.
/// </summary>
public class AuditLogMiddleware
{
    private readonly RequestDelegate _next;

    public AuditLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuditLogService auditLogService)
    {
        var userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        var userName = context.User?.Identity?.Name ?? "Anonymous";
        var path = context.Request.Path;
        var method = context.Request.Method;
        var ip = context.Connection.RemoteIpAddress?.ToString();

        var logMessage = $"{DateTime.UtcNow}: {method} {path} - User: {userName} ({userId}) - IP: {ip}";

        await auditLogService.LogAsync("Request", context.Request.Path, null, logMessage);

        await _next(context);
    }
}
