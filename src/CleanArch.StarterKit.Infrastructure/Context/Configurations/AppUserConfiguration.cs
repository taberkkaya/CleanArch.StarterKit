using CleanArch.StarterKit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Context.Configurations
{
    /// <summary>
    /// Configuration for AppUser (Identity user) entity mapping.
    /// </summary>
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Example: Email unique
            builder.HasIndex(u => u.Email).IsUnique();

            // Additional property configurations (optional)
        }
    }
}
