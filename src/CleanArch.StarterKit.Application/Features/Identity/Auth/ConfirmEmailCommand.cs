using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;
public sealed record ConfirmEmailCommand(
    Guid UserId,
    string Token
    ) : IRequest<Result<Unit>>;

internal sealed class ConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<ConfirmEmailCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<Unit>.Failure(new Error(ErrorCodes.NotFound, "Kullanıcı bulunamadı"));

        var result = await userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
            return Result<Unit>.Failure(new Error(ErrorCodes.NotFound, "E-posta doğrulanamadı, token geçersiz veya süresi dolmuş olabilir."));

        return Result<Unit>.Success(Unit.Value);
    }
}