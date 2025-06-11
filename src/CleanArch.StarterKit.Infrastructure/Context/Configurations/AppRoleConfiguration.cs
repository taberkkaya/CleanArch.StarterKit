using CleanArch.StarterKit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Context.Configurations
{
    /// <summary>
    /// Configuration for AppRole (Identity role) entity mapping.
    /// </summary>
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Example: Name is required and max length 256
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
