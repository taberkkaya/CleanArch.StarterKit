using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Roles;

/// <summary>
/// Command to update an existing role's properties.
/// </summary>
public sealed record UpdateRoleCommand(
    Guid Id,
    string Name
) : IRequest<Result<string>>;

/// <summary>
/// Handler that updates an existing application role,
/// clears the roles cache, and returns a confirmation message.
/// </summary>
internal sealed class UpdateRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager,
    ICacheService cacheService
) : IRequestHandler<UpdateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());

        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Role not found."));

        request.Adapt(role);

        await roleManager.UpdateAsync(role);

        cacheService.Remove("roles");

        return "Role has been updated successfully.";
    }
}
