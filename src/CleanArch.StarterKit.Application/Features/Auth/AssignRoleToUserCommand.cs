using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Auth;
public sealed record AssignRoleToUserCommand(
    Guid RoleId,
    Guid UserId
    ) : IRequest<Result<string>>;

internal sealed class AssignRoleToUserCommandHandler(
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<AssignRoleToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Role cannot found!"));

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found!"));

        await userManager.AddToRoleAsync(user,role.Name!);

        return "Assigned role to user";
    }
}
