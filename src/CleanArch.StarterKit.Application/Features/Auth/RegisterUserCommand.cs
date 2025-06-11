using MediatR;
using ResultKit;
using Microsoft.AspNetCore.Identity;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Application.Abstractions.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace CleanArch.StarterKit.Application.Features.Auth;

/// <summary>
/// Command for user registration.
/// </summary>
public class RegisterUserCommand : IRequest<Result<AuthRegisterResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    // Ekstra alan ekleyeceksen burada belirtebilirsin
}

/// <summary>
/// Response model for authentication endpoints.
/// </summary>
public class AuthRegisterResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}

/// <summary>
/// Handles user registration requests.
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthRegisterResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<AuthRegisterResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var error = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<AuthRegisterResponse>.Failure(new Error("RegisterError", error));
        }

        // Kullanıcının rolleri yoksa boş liste ver
        var token = _jwtTokenService.GenerateToken(user.Id,user.Email, new List<string>());

        return Result<AuthRegisterResponse>.Success(new AuthRegisterResponse
        {
            UserId = user.Id.ToString(),
            Email = user.Email,
            Token = token
        });
    }
}
