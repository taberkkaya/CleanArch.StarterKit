namespace CleanArch.StarterKit.Domain.Abstractions;

/// <summary>
/// Defines auditing properties for entities.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the creation date and time.
    /// </summary>
    DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    Guid? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the last modification date and time.
    /// </summary>
    DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// </summary>
    Guid? LastModifiedBy { get; set; }
}
