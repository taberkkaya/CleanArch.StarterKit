// /src/CleanArch.StarterKit.Application/DependencyInjection.cs

using System.Text;
using CleanArch.StarterKit.Application.Features.Auth;
using CleanArch.StarterKit.Application.Services;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArch.StarterKit.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Örnek: services.AddScoped<IService, Service>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommandHandler>());

        services.AddMapster();

        return services;
    }
}
