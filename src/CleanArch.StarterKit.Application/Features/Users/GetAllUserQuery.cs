using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Users;

public sealed record GetAllUserQuery() : IRequest<Result<List<ApplicationUser>>>;

internal sealed class GetAllUserQueryHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<GetAllUserQuery, Result<List<ApplicationUser>>>
{
    public async Task<Result<List<ApplicationUser>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.Include(p => p.UserRoles).ThenInclude(p => p.Role).ToListAsync();
        return users;
    }
}
