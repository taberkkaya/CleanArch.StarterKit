using System.Security.Claims;
using CleanArch.StarterKit.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace CleanArch.StarterKit.Infrastructure.Services
{
    /// <summary>
    /// Implementation of ICurrentUserService to obtain the current user from HttpContext.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public Guid? UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(userIdString, out var guid) ? guid : null;
            }
        }
    }
}
