using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;
public sealed record ResetPasswordRequestCommand(
    string Email) : IRequest<Result<string>>;

internal sealed class ResetPasswordRequestCommandHandler(
    UserManager<ApplicationUser> userManager,
    IEmailService emailService) : IRequestHandler<ResetPasswordRequestCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound,"Kullanıcı bulunamadı"));

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        // Token URL-safe hale getir
        var urlToken = Uri.EscapeDataString(token);

        // Uygulamanın frontend adresini buraya koy, örnek için:
        var resetLink = $"https://localhost:7294/api/auth/resetpassword?userId={user.Id}&token={urlToken}";

        await emailService.SendAsync(
            user.Email,
            "Şifre Sıfırlama",
            $"Şifrenizi sıfırlamak için <a href='{resetLink}'>tıklayın</a>.",
            true
        );

        return "Parola sıfırlama maili gönderildi!";
    }
}

