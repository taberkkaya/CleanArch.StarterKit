using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Infrastructure.Persistence;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.StarterKit.Infrastructure.Filters;

/// <summary>
/// Provides Basic Authentication authorization for Hangfire Dashboard access.
/// </summary>
public class BasicAuthAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var header = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (header != null && header.StartsWith("Basic "))
        {
            var encodedUsernamePassword = header.Substring("Basic ".Length).Trim();
            var usernamePassword = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            var parts = usernamePassword.Split(':');
            if (parts.Length == 2)
            {
                var username = parts[0];
                var password = parts[1];

                var scopeFactory = httpContext.RequestServices.GetService<IServiceScopeFactory>();
                using var scope = scopeFactory!.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var user = dbContext.HangfireDashboardUsers.FirstOrDefault(u => u.UserName == username && u.IsActive);

                if (user != null)
                {
                    var hasher = scope.ServiceProvider.GetRequiredService<IDashboardPasswordHasher>();
                    if (hasher.VerifyHashedPassword(user.PasswordHash, password))
                        return true;
                }
            }
        }
        httpContext.Response.Headers["WWW-Authenticate"] = "Basic";
        httpContext.Response.StatusCode = 401;
        return false;
    }
}
