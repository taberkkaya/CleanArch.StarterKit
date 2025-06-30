using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

public sealed record LoginUserCommandResponse(string token);

public sealed record LoginUserCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<LoginUserCommandResponse>>;

internal sealed class LoginUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService
    ) : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(p => p.Email == request.EmailOrUserName || p.UserName == request.EmailOrUserName);
        if (user == null || user.IsDeleted)
            return Result<LoginUserCommandResponse>.Failure(new Error(ErrorCodes.NotFound, "Kullanıcı bulunamadı veya silinmiş."));

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result<LoginUserCommandResponse>.Failure(new Error(ErrorCodes.Unauthorized, "Şifre hatalı!"));

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.UserName ?? user.Email ?? "", user.Email ?? "", roles);

        return new LoginUserCommandResponse(token);
    }
}
