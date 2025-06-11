using System;
using CleanArch.StarterKit.Domain.Abstractions;

namespace CleanArch.StarterKit.Domain.Entities;

/// <summary>
/// Represents a product entity with audit fields.
/// </summary>
public class Product : IAuditableEntity
{
    /// <summary>
    /// Gets or sets the product identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    public DateTime CreatedDate { get; set; }

    /// <inheritdoc/>
    public Guid? CreatedBy { get; set; }

    /// <inheritdoc/>
    public DateTime? LastModifiedDate { get; set; }

    /// <inheritdoc/>
    public Guid? LastModifiedBy { get; set; }
}
