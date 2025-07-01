namespace CleanArch.StarterKit.Domain.Entities;

/// <summary>
/// Defines properties for tracking audit information on entities.
/// </summary>
public interface IUserAuditable
{
    /// <summary>
    /// The date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who created the entity.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// The date and time when the entity was last updated.
    /// </summary>
    DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who last updated the entity.
    /// </summary>
    string? UpdatedBy { get; set; }

    /// <summary>
    /// The date and time when the entity was deleted.
    /// </summary>
    DateTime? DeletedAt { get; set; }

    /// <summary>
    /// The identifier of the user who deleted the entity.
    /// </summary>
    string? DeletedBy { get; set; }

    /// <summary>
    /// Indicates whether the entity has been marked as deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}
