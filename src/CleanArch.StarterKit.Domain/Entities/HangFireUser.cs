namespace CleanArch.StarterKit.Domain.Entities;

/// <summary>
/// Represents a Hangfire dashboard user with credentials and activation status.
/// </summary>
public class HangFireUser
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The username used to log in to the dashboard.
    /// </summary>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// The hashed password. Always store hashed!
    /// </summary>
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Indicates whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
