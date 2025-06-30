using CleanArch.StarterKit.Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Users;
public sealed record CreateUserCommand(
    string Email,
    string Password
    ) : IRequest<Result<string>>;

internal sealed class CreateUserCommandHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async  Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<string>.ValidationFailure(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));

        return "User registration completed successfully.";
    }
}
