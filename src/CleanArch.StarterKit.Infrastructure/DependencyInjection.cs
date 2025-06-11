using System.Reflection;
using System.Text;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RepositoryKit.Core.Interfaces;
using RepositoryKit.EntityFramework.Implementations;
using Scrutor;

namespace CleanArch.StarterKit.Infrastructure
{
    /// <summary>
    /// Registers infrastructure layer services, repositories, authentication, and DbContext for dependency injection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds all infrastructure services, repositories, authentication, and DbContext to the DI container.
        /// </summary>
        /// <param name="services">The service collection to add dependencies to.</param>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>The service collection with infrastructure services registered.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with SQL Server connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(sp => new EfUnitOfWork<ApplicationDbContext>(sp.GetRequiredService<ApplicationDbContext>()));

            // Register Identity services (UserManager, RoleManager, SignInManager, etc.)
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Automatically register all repositories and services in the assembly using Scrutor
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

            // Register JWT authentication and authorization
            var jwtSettings = configuration.GetSection("Jwt");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
                };
            });


            services.AddAuthorization();

            // Register HttpContextAccessor (for CurrentUserService, etc.)
            services.AddHttpContextAccessor();

            return services;
        }

        /// <summary>
        /// Applies pending migrations to the database if the database does not exist but migrations exist,
        /// and optionally seeds initial data (e.g., admin user).
        /// </summary>
        /// <typeparam name="TContext">The application's DbContext.</typeparam>
        /// <param name="host">The application host.</param>
        /// <returns>The application host.</returns>
        public static async Task<IHost> MigrateAndSeedDatabaseAsync<TContext>(this IHost host)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
                try
                {
                    // Only migrate if the database does not exist and there are migrations
                    var databaseCreator = dbContext.Database.GetService<IRelationalDatabaseCreator>();
                    if (!await databaseCreator.ExistsAsync())
                    {
                        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                        if (pendingMigrations.Any())
                        {
                            await dbContext.Database.MigrateAsync();
                        }
                    }
                    else
                    {
                        // If DB exists but there are pending migrations, apply them
                        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                        if (pendingMigrations.Any())
                        {
                            await dbContext.Database.MigrateAsync();
                        }
                    }

                    // Seed initial data (e.g., first admin user)
                    if (typeof(TContext) == typeof(ApplicationDbContext))
                    {
                        await SeedInitialUsersAsync(scope.ServiceProvider);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Replace with logger
                    Console.WriteLine($"Database migration/seed failed: {ex.Message}");
                    throw;
                }
            }
            return host;
        }

        /// <summary>
        /// Seeds the initial users/roles if they do not exist.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        private static async Task SeedInitialUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            string adminRole = "Admin";
            string adminEmail = "admin@starterkit.local";
            string adminPassword = "Admin123!"; // TODO: Set strong password & secure in production

            // Create role if not exists
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new AppRole { Name = adminRole });
            }

            // Create admin user if not exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                // else: log error or throw
            }
        }
    }
}
