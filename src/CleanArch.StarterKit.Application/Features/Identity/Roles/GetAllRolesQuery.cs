using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Roles;
public sealed record GetAllRolesQuery() : IRequest<Result<List<ApplicationRole>>>;

internal sealed class GetAllRolesQueryHandler(
    RoleManager<ApplicationRole> roleManager,
    ICacheService cacheService
    ) : IRequestHandler<GetAllRolesQuery, Result<List<ApplicationRole>>>
{
    public async Task<Result<List<ApplicationRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = cacheService.Get<List<ApplicationRole>>("roles");

        if (roles is null)
        {
            roles = await roleManager.Roles.ToListAsync(cancellationToken);
            cacheService.Set("roles", roles);
        }

        return roles;
    }
}
