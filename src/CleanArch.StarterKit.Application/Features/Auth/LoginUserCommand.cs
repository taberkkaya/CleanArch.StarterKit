using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Auth;

public sealed record LoginUserCommandResponse(string token);

public sealed record LoginUserCommand(
    string Email,
    string Password) : IRequest<Result<LoginUserCommandResponse>>;

internal sealed class LoginUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService
    ) : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || user.IsDeleted)
            return Result<LoginUserCommandResponse>.Failure(new Error("404", "Kullanıcı bulunamadı veya silinmiş."));

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result<LoginUserCommandResponse>.Failure(new Error("401", "Şifre hatalı!"));

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.UserName ?? user.Email ?? "", user.Email ?? "", roles);

        return new LoginUserCommandResponse(token);
    }
}
