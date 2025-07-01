using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Users;

public sealed record GetAllUserQuery() : IRequest<Result<List<ApplicationUser>>>;

internal sealed class GetAllUserQueryHandler(
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService
    ) : IRequestHandler<GetAllUserQuery, Result<List<ApplicationUser>>>
{
    public async Task<Result<List<ApplicationUser>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = cacheService.Get<List<ApplicationUser>>("users");

        if (users == null) { 
            users = await userManager.Users.ToListAsync();
            cacheService.Set("users",users);
        }

        return users;
    }
}
