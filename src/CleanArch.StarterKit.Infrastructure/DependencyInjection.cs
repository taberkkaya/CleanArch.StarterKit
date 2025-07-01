using CleanArch.StarterKit.Application.Repositories;
using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using CleanArch.StarterKit.Infrastructure.Persistence;
using CleanArch.StarterKit.Infrastructure.Repositories;
using CleanArch.StarterKit.Infrastructure.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RepositoryKit.Core.Interfaces;
using Scrutor;
using System.Reflection;

namespace CleanArch.StarterKit.Infrastructure;

/// <summary>
/// Provides extension methods to register infrastructure-layer dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services, including DbContext, Identity, caching, health checks, and custom services.
    /// </summary>
    /// <param name="services">The service collection to add dependencies to.</param>
    /// <param name="connectionString">The database connection string.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)); // or UseNpgsql, UseSqlite

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddHttpContextAccessor();
        services.AddMemoryCache();

        // Health Checks
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("DbContext")
            .AddSqlServer(connectionString, name: "SqlServer");

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        services.Scan(action =>
        {
            action
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .AsImplementedInterfaces()
            .WithScopedLifetime();
        });

        return services;
    }
}
