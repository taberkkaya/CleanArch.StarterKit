using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;
public sealed record SendConfirmEmailCommand(
    Guid UserId
) : IRequest<Result<string>>;


internal sealed class SendConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailService emailService
) : IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found!"));

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"https://localhost:7294/api/Auth/ConfirmEmail?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        if(user.Email is null)
        {
            return Result<string>.Failure(new Error(ErrorCodes.NotFound,"Email not found!"));
        }

        await emailService.SendAsync(user.Email,"Email doğrulama",confirmationLink);

        return "Mail gönderimi başarılı";
    }
}
