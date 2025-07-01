using CleanArch.StarterKit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configures the AuditLog entity mapping to the database.
    /// </summary>
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.Property(x => x.Timestamp).IsRequired();
        }
    }
}
