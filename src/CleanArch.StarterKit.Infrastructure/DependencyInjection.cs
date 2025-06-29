using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using CleanArch.StarterKit.Infrastructure.Persistence;
using CleanArch.StarterKit.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
