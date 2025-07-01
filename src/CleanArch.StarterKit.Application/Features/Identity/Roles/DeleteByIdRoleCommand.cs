using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Roles;

/// <summary>
/// Command to delete a role by its unique identifier.
/// </summary>
public sealed record DeleteByIdRoleCommand(
    Guid Id) : IRequest<Result<string>>;

/// <summary>
/// Handler that processes deleting a role from the system,
/// clears the roles cache, and returns a confirmation message.
/// </summary>
internal sealed class DeleteByIdRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager,
    ICacheService cacheService
    ) : IRequestHandler<DeleteByIdRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteByIdRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());

        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Role not found."));

        await roleManager.DeleteAsync(role);

        cacheService.Remove("roles");

        return "Role has been deleted successfully.";
    }
}
