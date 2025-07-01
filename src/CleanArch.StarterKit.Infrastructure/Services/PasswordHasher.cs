using CleanArch.StarterKit.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.StarterKit.Infrastructure.Services;

/// <summary>
/// Provides password hashing and verification for Hangfire dashboard users.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Generates a hashed representation of the provided password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password string.</returns>
    public string HashPassword(string password)
    {
        return new PasswordHasher<object>().HashPassword(null!, password);
    }

    /// <summary>
    /// Verifies that a provided password matches the stored hashed password.
    /// </summary>
    /// <param name="hashedPassword">The hashed password to compare against.</param>
    /// <param name="providedPassword">The plain text password to verify.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return new PasswordHasher<object>().VerifyHashedPassword(null!, hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
    }
}
