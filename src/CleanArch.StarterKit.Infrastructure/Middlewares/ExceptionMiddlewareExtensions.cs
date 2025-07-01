using Microsoft.AspNetCore.Builder;

namespace CleanArch.StarterKit.Infrastructure.Middlewares;

/// <summary>
/// Extension methods for registering the exception handling middleware.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Adds the <see cref="ExceptionHandlingMiddleware"/> to the application's request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The updated application builder.</returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
