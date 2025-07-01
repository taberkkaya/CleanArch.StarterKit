namespace CleanArch.StarterKit.Application.Services;

/// <summary>
/// Service for generating JWT tokens for authenticated users.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token containing user identity and role claims.
    /// </summary>
    /// <param name="userId">The user's unique identifier.</param>
    /// <param name="userName">The user's username.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="roles">The list of roles assigned to the user.</param>
    /// <returns>A signed JWT token string.</returns>
    string GenerateToken(string userId, string userName, string email, IList<string> roles);
}
