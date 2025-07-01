using CleanArch.StarterKit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configures the HangFireUser entity mapping to the database.
    /// </summary>
    public class HangFireUserConfiguration : IEntityTypeConfiguration<HangFireUser>
    {
        public void Configure(EntityTypeBuilder<HangFireUser> builder)
        {
            builder.ToTable("Users", schema: "HangFire"); // Table: HangFire.Users

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(x => x.PasswordHash)
                .IsRequired();
        }
    }
}
