using CleanArch.StarterKit.Infrastructure.BackgroundJobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.StarterKit.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for configuring and registering Hangfire background jobs.
    /// </summary>
    public static class HangfireRegistrationExtensions
    {
        /// <summary>
        /// Adds Hangfire services and registers custom job services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">The connection string for Hangfire storage.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddCustomHangfireJobs(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<AuditLogCleanupJob>();

            // Other job services...

            return services;
        }

        /// <summary>
        /// Configures recurring Hangfire jobs.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseCustomHangfireJobs(this IApplicationBuilder app)
        {
            // Job registrations are here
            RecurringJob.AddOrUpdate<AuditLogCleanupJob>(
                "auditlog-cleanup",
                job => job.CleanOldLogsAsync(),
                Cron.Daily
            );

            // Other job registrations...
        }
    }
}
