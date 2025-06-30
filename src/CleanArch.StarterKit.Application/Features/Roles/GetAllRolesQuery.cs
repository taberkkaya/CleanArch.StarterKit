using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.StarterKit.Application.Features.Roles;
public sealed record GetAllRolesQuery() : IRequest<Result<List<ApplicationRole>>>;

internal sealed class GetAllRolesQueryHandler(
    RoleManager<ApplicationRole> roleManager
    ) : IRequestHandler<GetAllRolesQuery, Result<List<ApplicationRole>>>
{
    public async Task<Result<List<ApplicationRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync(cancellationToken);
        return roles;
    }
}
