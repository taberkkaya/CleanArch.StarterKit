using MediatR;
using Microsoft.AspNetCore.Identity;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Application.Abstractions.Services;
using ResultKit;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

/// <summary>
/// Command and handler for user login.
/// </summary>
namespace CleanArch.StarterKit.Application.Features.Auth;

/// <summary>
/// Command for logging in a user.
/// </summary>
public class LoginUserCommand : IRequest<Result<AuthLoginResponse>>
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User password.
    /// </summary>
    public string Password { get; set; }
}

/// <summary>
/// Response model for authentication endpoints.
/// </summary>
public class AuthLoginResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}

/// <summary>
/// Handles user login requests.
/// </summary>
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthLoginResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUserCommandHandler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<AuthLoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<AuthLoginResponse>.Failure(new Error("LoginError", "User not found."));

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!signInResult.Succeeded)
            return Result<AuthLoginResponse>.Failure(new Error("LoginError", "Invalid credentials."));

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user.Id, user.Email, roles);

        return Result<AuthLoginResponse>.Success(new AuthLoginResponse
        {
            UserId = user.Id.ToString(),
            Email = user.Email,
            Token = token
        });
    }
}
