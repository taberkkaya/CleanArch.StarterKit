using CleanArch.StarterKit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.StarterKit.Infrastructure.Context.Configurations
{
    /// <summary>
    /// Configuration for Product entity mapping.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Audit fields
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.CreatedBy);
            builder.Property(x => x.LastModifiedDate);
            builder.Property(x => x.LastModifiedBy);
        }
    }
}
