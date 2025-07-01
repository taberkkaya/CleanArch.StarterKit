using CleanArch.StarterKit.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Command to confirm the email address of a user using a provided confirmation token.
/// </summary>
public sealed record ConfirmEmailCommand(
    Guid UserId,
    string Token
    ) : IRequest<Result<Unit>>;

/// <summary>
/// Handler that processes the confirmation of a user's email.
/// It verifies the user exists and attempts to confirm the email with the token.
/// Returns success or failure accordingly.
/// </summary>
internal sealed class ConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager
) : IRequestHandler<ConfirmEmailCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<Unit>.Failure(new Error(ErrorCodes.NotFound, "User not found."));

        var result = await userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
            return Result<Unit>.Failure(new Error(ErrorCodes.Conflict, "Email could not be confirmed. The token may be invalid or expired."));

        return Result<Unit>.Success(Unit.Value);
    }
}
