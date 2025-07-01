using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using CleanArch.StarterKit.Infrastructure.Persistence;
using CleanArch.StarterKit.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArch.StarterKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)); // veya UseNpgsql, UseSqlite

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();
        services.AddMemoryCache();

        // Health Checks
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("DbContext")
            .AddSqlServer(connectionString, name : "SqlServer");

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        // Custom Services
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ICacheService, MemoryCacheService>();
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;
    }
}
