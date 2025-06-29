using CleanArch.StarterKit.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;
using Serilog;

namespace CleanArch.StarterKit.Application.Features.Auth;
public sealed record RegisterUserCommand(
    string Email,
    string Password
    ) : IRequest<Result<string>>
{

}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

 

        if (!result.Succeeded)
            return Result<string>.ValidationFailure(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));
            Log.Error("Error while processing request for UserId: {UserId}", "system");

        return "User registration completed successfully.";
    }
}
