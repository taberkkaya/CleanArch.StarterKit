using CleanArch.StarterKit.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Persistence.Configurations.Identity
{
    /// <summary>
    /// Configures the ApplicationUser entity mapping to the database.
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.EmailConfirmed)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(50);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
        }
    }
}
