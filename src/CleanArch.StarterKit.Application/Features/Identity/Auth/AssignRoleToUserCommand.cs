using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Command to assign a specific role to a user by their IDs.
/// </summary>
public sealed record AssignRoleToUserCommand(
    Guid RoleId,
    Guid UserId
    ) : IRequest<Result<string>>;

/// <summary>
/// Handler that processes assigning a role to a user.
/// It validates the existence of the role and user,
/// assigns the role, removes related cache entries,
/// and returns a success message.
/// </summary>
internal sealed class AssignRoleToUserCommandHandler(
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService
    ) : IRequestHandler<AssignRoleToUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Role not found."));

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found."));

        await userManager.AddToRoleAsync(user, role.Name!);

        cacheService.Remove("users");

        return "Role has been successfully assigned to the user.";
    }
}
