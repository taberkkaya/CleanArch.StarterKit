// /src/CleanArch.StarterKit.Application/DependencyInjection.cs

using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.StarterKit.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Örnek: services.AddScoped<IService, Service>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddMapster();

        return services;
    }
}
