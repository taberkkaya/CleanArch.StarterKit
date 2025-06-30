using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Users;
public sealed record DeleteByIdUserCommand(
    Guid Id
    ) : IRequest<Result<string>>;

internal sealed class DeleteByIdUserCommandHandler(
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<DeleteByIdUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteByIdUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found!"));

        await userManager.DeleteAsync(user);

        return "User deleted done";
    }
}
