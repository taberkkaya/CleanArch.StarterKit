using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Roles;

/// <summary>
/// Command to create a new role with the specified name.
/// </summary>
public sealed record CreateRoleCommand(string Name) : IRequest<Result<string>>;

/// <summary>
/// Handler that processes creating a new application role,
/// clears the cache, and returns a confirmation message.
/// </summary>
internal sealed class CreateRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager,
    ICacheService cacheService
    ) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var isRoleExists = await roleManager.RoleExistsAsync(request.Name);
        if (isRoleExists)
            return Result<string>.Failure(new Error("409", $"Role '{request.Name}' already exists."));

        var role = request.Adapt<ApplicationRole>();

        await roleManager.CreateAsync(role);

        cacheService.Remove("roles");

        return $"Role '{role.Name}' has been created successfully.";
    }
}
