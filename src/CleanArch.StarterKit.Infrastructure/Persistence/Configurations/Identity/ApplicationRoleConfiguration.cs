using CleanArch.StarterKit.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Persistence.Configurations.Identity;

/// <summary>
/// Configures the ApplicationRole entity mapping to the database.
/// </summary>
public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name).IsUnique();

        // If you have added custom fields, configure them here as well
    }
}
