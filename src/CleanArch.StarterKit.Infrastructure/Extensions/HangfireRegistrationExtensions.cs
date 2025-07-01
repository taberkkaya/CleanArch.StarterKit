// Infrastructure/Extensions/HangfireRegistrationExtensions.cs

using CleanArch.StarterKit.Infrastructure.BackgroundJobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.StarterKit.Infrastructure.Extensions
{
    public static class HangfireRegistrationExtensions
    {
        public static IServiceCollection AddCustomHangfireJobs(this IServiceCollection services,string connectionString)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<AuditLogCleanupJob>();

            // Diğer job servisleri...

            return services;
        }

        public static void UseCustomHangfireJobs(this IApplicationBuilder app)
        {
            // Job registrationları burada
            RecurringJob.AddOrUpdate<AuditLogCleanupJob>(
                "auditlog-cleanup",
                job => job.CleanOldLogsAsync(),
                Cron.Daily
            );

            // Diğer job registrationları...
        }
    }
}
