using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace CleanArch.StarterKit.Application
{
    /// <summary>
    /// Registers application layer services such as MediatR (CQRS).
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds application services including MediatR to the DI container.
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
