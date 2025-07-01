using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Command to request a password reset link for a user identified by email.
/// </summary>
public sealed record ResetPasswordRequestCommand(
    string Email) : IRequest<Result<string>>;

/// <summary>
/// Handler that generates a password reset token and sends it via email.
/// </summary>
internal sealed class ResetPasswordRequestCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailService emailService) : IRequestHandler<ResetPasswordRequestCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found."));

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        // Make token URL-safe
        var urlToken = Uri.EscapeDataString(token);

        // Put your application's frontend URL here
        var resetLink = $"https://localhost:7294/api/auth/resetpassword?userId={user.Id}&token={urlToken}";

        await emailService.SendAsync(
            user.Email!,
            "Password Reset",
            $"To reset your password, please <a href='{resetLink}'>click here</a>.",
            true
        );

        return "Password reset email has been sent successfully.";
    }
}
