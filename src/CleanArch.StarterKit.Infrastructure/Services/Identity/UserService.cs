using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Application.Services.Identity;
using CleanArch.StarterKit.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ResultKit;

namespace CleanArch.StarterKit.Infrastructure.Services.Identity;

/// <summary>
/// Service responsible for user-related operations such as email confirmation token generation and sending confirmation emails.
/// </summary>
internal sealed class UserService(
    UserManager<ApplicationUser> userManager,
    IEmailService emailService,
    IConfiguration configuration) : IUserService
{
    /// <summary>
    /// Generates an email confirmation token for the specified user.
    /// </summary>
    public async Task<Result<string>> GetConfirmEmailToken(ApplicationUser user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }

    /// <summary>
    /// Sends an email confirmation message to the specified user.
    /// </summary>
    public async Task<string> SendConfirmEmail(ApplicationUser user)
    {
        var token = GetConfirmEmailToken(user).Result.Value;

        var baseUrl = configuration["App:BaseUrl"];
        var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token!)}";

        await emailService.SendAsync(
            user.Email!,
            "Email Confirmation",
            $"To confirm your email address, please <a href='{confirmationLink}'>click here</a>.",
            true
        );

        return "Email confirmation message has been sent successfully.";
    }
}
