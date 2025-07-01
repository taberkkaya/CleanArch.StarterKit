using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Command to send an email confirmation link to a user.
/// </summary>
public sealed record SendConfirmEmailCommand(
    Guid UserId
) : IRequest<Result<string>>;

/// <summary>
/// Handler that generates an email confirmation token and sends it via email to the user.
/// </summary>
internal sealed class SendConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailService emailService,
    IConfiguration configuration
) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found."));

        if (user.Email is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Email address not found."));

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var baseUrl = configuration["App:BaseUrl"];
        var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        await emailService.SendAsync(
            user.Email,
            "Email Confirmation",
            $"To confirm your email address, please <a href='{confirmationLink}'>click here</a>.",
            true
        );

        return "Email confirmation message has been sent successfully.";
    }
}
