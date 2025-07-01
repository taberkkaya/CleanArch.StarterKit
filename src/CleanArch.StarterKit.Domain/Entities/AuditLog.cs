using CleanArch.StarterKit.Domain.Abstractions;

namespace CleanArch.StarterKit.Domain.Entities;

/// <summary>
/// Represents an audit log entry for tracking changes or actions performed in the system.
/// </summary>
public class AuditLog : BaseEntity<int>
{
    /// <summary>
    /// The identifier of the user who performed the action.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The username of the user who performed the action.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// The action performed (e.g., Create, Update, Delete).
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// The name of the table affected by the action.
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// The previous values in JSON format before the change.
    /// </summary>
    public string? OldValues { get; set; }

    /// <summary>
    /// The new values in JSON format after the change.
    /// </summary>
    public string? NewValues { get; set; }

    /// <summary>
    /// The IP address from which the action was performed.
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// The timestamp indicating when the action occurred.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
