using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Domain.Identity;
using CleanArch.StarterKit.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RepositoryKit.Core.Interfaces;
using RepositoryKit.EntityFramework.Implementations;
using System.Security.Claims;

namespace CleanArch.StarterKit.Infrastructure.Persistence;

/// <summary>
/// The application's Entity Framework Core DbContext, integrating Identity, soft deletes, and audit logging.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IUnitOfWork<ApplicationDbContext>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // DbSets
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<HangfireDashboardUser> HangfireDashboardUsers { get; set; }

    public override int SaveChanges()
    {
        AddAuditLogs();
        HandleAuditAndSoftDelete();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddAuditLogs();
        HandleAuditAndSoftDelete();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Handles audit fields and implements soft delete behavior.
    /// </summary>
    private void HandleAuditAndSoftDelete()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name;

        foreach (var entry in ChangeTracker.Entries<IUserAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userId ?? userName ?? "SYSTEM";
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId ?? userName ?? "SYSTEM";
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userId ?? userName ?? "SYSTEM";
                    break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ignore Identity entities you do not use
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        // builder.Ignore<IdentityUserRole<Guid>>();

        // Configure global query filter for soft deletes
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IUserAuditable).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetSoftDeleteQueryFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, new object[] { builder });
            }
        }
    }

    /// <summary>
    /// Sets a global query filter to exclude soft-deleted entities.
    /// </summary>
    private static void SetSoftDeleteQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IUserAuditable
    {
        builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }

    /// <summary>
    /// Creates audit logs for tracked entity changes.
    /// </summary>
    private void AddAuditLogs()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .ToList())
        {
            // Skip audit log entries themselves
            if (entry.Entity is AuditLog) continue;

            var tableName = entry.Entity.GetType().Name;

            string? oldValues = null, newValues = null;
            if (entry.State == EntityState.Modified)
            {
                oldValues = System.Text.Json.JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                newValues = System.Text.Json.JsonSerializer.Serialize(entry.CurrentValues.ToObject());
            }
            else if (entry.State == EntityState.Added)
            {
                newValues = System.Text.Json.JsonSerializer.Serialize(entry.CurrentValues.ToObject());
            }
            else if (entry.State == EntityState.Deleted)
            {
                oldValues = System.Text.Json.JsonSerializer.Serialize(entry.OriginalValues.ToObject());
            }

            AuditLogs.Add(new AuditLog
            {
                UserId = userId,
                UserName = userName,
                IpAddress = ip,
                Action = entry.State.ToString(),
                TableName = tableName,
                OldValues = oldValues,
                NewValues = newValues,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Returns a repository instance for the specified entity type.
    /// </summary>
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return new EfRepository<TEntity, ApplicationDbContext>(this);
    }
}
