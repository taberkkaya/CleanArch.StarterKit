using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Response containing the generated JWT token after successful login.
/// </summary>
public sealed record LoginUserCommandResponse(string token);

/// <summary>
/// Command to log in a user using email or username and password.
/// </summary>
public sealed record LoginUserCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<LoginUserCommandResponse>>;

/// <summary>
/// Handler that processes user login by verifying credentials,
/// generating a JWT token, and returning it upon success.
/// </summary>
internal sealed class LoginUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService
    ) : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(p => p.Email == request.EmailOrUserName || p.UserName == request.EmailOrUserName);
        if (user == null || user.IsDeleted)
            return Result<LoginUserCommandResponse>.Failure(new Error(ErrorCodes.NotFound, "User not found or has been deleted."));

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result<LoginUserCommandResponse>.Failure(new Error(ErrorCodes.Unauthorized, "Invalid password."));

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenService.GenerateToken(user.Id.ToString(), user.UserName ?? user.Email ?? "", user.Email ?? "", roles);

        return new LoginUserCommandResponse(token);
    }
}
