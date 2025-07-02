using CleanArch.StarterKit.Domain.Entities.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Services.Identity
{
    /// <summary>
    /// Defines user-related service operations such as generating email confirmation tokens and sending confirmation emails.
    /// </summary>
    public interface IUserService
    {
        Task<Result<string>> GetConfirmEmailToken(ApplicationUser user);
        Task<string> SendConfirmEmail(ApplicationUser user);
    }
}
