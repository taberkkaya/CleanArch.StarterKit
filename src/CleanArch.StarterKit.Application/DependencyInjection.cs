// /src/CleanArch.StarterKit.Application/DependencyInjection.cs

using CleanArch.StarterKit.Application.Features.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.StarterKit.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Örnek: services.AddScoped<IService, Service>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommandHandler>());

 


        return services;
    }
}
