using System.Security.Claims;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.StarterKit.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor
    ) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Henüz DbSet yok

    public override int SaveChanges()
    {
        HandleAuditAndSoftDelete();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleAuditAndSoftDelete();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void HandleAuditAndSoftDelete()
    {
        // Kullanıcı adı/id’si
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

        // Kullanmadıklarını ignore et
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        //builder.Ignore<IdentityUserRole<Guid>>();

        builder.Entity<ApplicationUser>()
       .HasMany(u => u.Roles)
       .WithMany(r => r.Users)
       .UsingEntity<IdentityUserRole<Guid>>(
           // Ara varlık üzerinde Role yönlendirmesi
           ur => ur.HasOne<ApplicationRole>()
                   .WithMany()
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired(),
           // Ara varlık üzerinde User yönlendirmesi
           ur => ur.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired());


        // Audit/soft delete query filter
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

    private static void SetSoftDeleteQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IUserAuditable
    {
        builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }
}
