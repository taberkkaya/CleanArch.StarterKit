using CleanArch.StarterKit.Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Roles;
public sealed record CreateRoleCommand(string Name) : IRequest<Result<string>>;

internal sealed class CreateRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<CreateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var isRoleExists = await roleManager.RoleExistsAsync(request.Name);
        if (isRoleExists)
            return Result<string>.Failure(new Error("404", $"Role '{request.Name}' already exists."));

        var role = request.Adapt<ApplicationRole>();

        await roleManager.CreateAsync(role);

        return $"Role '{role.Name}' created successfully.";
    }
}
