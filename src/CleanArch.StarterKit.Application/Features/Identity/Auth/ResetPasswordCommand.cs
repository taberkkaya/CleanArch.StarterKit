using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;
public sealed record ResetPasswordCommand(Guid UserId, string Token, string NewPassword) : IRequest<Result>;

internal sealed class ResetPasswordCommandHandler(
    UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Kullanıcı bulunamadı"));

        // Token URL'den decode edilmeli
        var decodedToken = Uri.UnescapeDataString(request.Token);

        var result = await userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

        if (!result.Succeeded)
            return Result<string>.Failure(new Error(ErrorCodes.Validation, "Şifre sıfırlanamadı: " + string.Join(", ", result.Errors.Select(x => x.Description))));

        return Result.Success();
    }
}
