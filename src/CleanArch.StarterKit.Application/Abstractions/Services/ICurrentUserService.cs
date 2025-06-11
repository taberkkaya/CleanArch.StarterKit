using System;

namespace CleanArch.StarterKit.Application.Abstractions.Services
{
    /// <summary>
    /// Provides the currently authenticated user's information.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the current user's Id, if available.
        /// </summary>
        Guid? UserId { get; }
    }
}
