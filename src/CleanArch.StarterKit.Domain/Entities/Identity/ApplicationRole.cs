using CleanArch.StarterKit.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.StarterKit.Domain.Entities.Identity;

/// <summary>
/// Represents an application role with audit metadata.
/// </summary>
public class ApplicationRole : IdentityRole<Guid>, IUserAuditable
{
    /// <summary>
    /// The date and time when the role was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who created the role.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// The date and time when the role was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// The identifier of the user who last updated the role.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// The date and time when the role was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// The identifier of the user who deleted the role.
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// Indicates whether the role has been marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}
