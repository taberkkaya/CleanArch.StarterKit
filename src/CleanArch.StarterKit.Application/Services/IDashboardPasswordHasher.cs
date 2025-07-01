namespace CleanArch.StarterKit.Application.Services;

/// <summary>
/// Service for hashing and verifying dashboard user passwords.
/// </summary>
public interface IDashboardPasswordHasher
{
    /// <summary>
    /// Generates a hashed representation of the provided password.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <returns>The hashed password string.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies that a provided password matches the hashed password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password.</param>
    /// <param name="providedPassword">The plain text password to verify.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}
