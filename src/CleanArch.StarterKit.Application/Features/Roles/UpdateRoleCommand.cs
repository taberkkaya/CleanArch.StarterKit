using CleanArch.StarterKit.Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.StarterKit.Application.Features.Roles;
public sealed record UpdateRoleCommand(
    Guid Id,
    string Name
    ) : IRequest<Result<string>>;

internal sealed class UpdateRoleCommandHandler(
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<UpdateRoleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());

        if (role is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "Role cannot found!"));

        request.Adapt(role);

        await roleManager.UpdateAsync(role);

        return "Role updated";
    }
}
