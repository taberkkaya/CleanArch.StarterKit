using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ResultKit;
using Serilog;

namespace CleanArch.StarterKit.Infrastructure.Middlewares;

/// <summary>
/// Middleware that handles unhandled exceptions globally and returns a standardized JSON error response.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception with Serilog
            Log.Error(ex, "Unhandled exception occurred. Path: {Path}", context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new Error(ErrorCodes.Conflict, exception.Message));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
