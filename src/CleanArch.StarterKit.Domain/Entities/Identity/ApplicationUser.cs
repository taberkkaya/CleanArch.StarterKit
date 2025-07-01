using CleanArch.StarterKit.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.StarterKit.Domain.Entities.Identity;

/// <summary>
/// Represents an application user with audit metadata.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IUserAuditable
{
    /// <summary>
    /// The date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who created this record.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// The date and time when the user was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who last updated this record.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// The date and time when the user was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// The identifier of the user who deleted this record.
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// Indicates whether the user has been marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}
