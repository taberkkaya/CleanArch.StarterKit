using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Roles;
public sealed record DeleteByIdRoleCommand(
    Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteByIdRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<DeleteByIdRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteByIdRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());
        
        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound,"Role cannot found!"));

        await roleManager.DeleteAsync(role);

        return "Role deleted done";
    }
}
