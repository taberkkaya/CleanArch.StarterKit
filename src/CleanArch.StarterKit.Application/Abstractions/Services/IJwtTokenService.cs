using System;
using System.Collections.Generic;

namespace CleanArch.StarterKit.Application.Abstractions.Services
{
    /// <summary>
    /// Provides JWT token generation for authentication.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user and roles.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="email">User email.</param>
        /// <param name="roles">List of user roles.</param>
        /// <returns>JWT token as string.</returns>
        string GenerateToken(Guid userId, string email, IEnumerable<string> roles);
    }
}
