using CleanArch.StarterKit.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.StarterKit.Application.Features.Identity.Auth;

/// <summary>
/// Command to reset a user's password using a reset token and a new password.
/// </summary>
public sealed record ResetPasswordCommand(Guid UserId, string Token, string NewPassword) : IRequest<Result>;

/// <summary>
/// Handler that processes resetting the password of a user.
/// It decodes the token, performs the reset operation,
/// and returns success or validation error messages.
/// </summary>
internal sealed class ResetPasswordCommandHandler(
    UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found."));

        // The token should be decoded if it comes from the URL
        var decodedToken = Uri.UnescapeDataString(request.Token);

        var result = await userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

        if (!result.Succeeded)
            return Result<string>.Failure(new Error(ErrorCodes.Validation, "Password could not be reset: " + string.Join(", ", result.Errors.Select(x => x.Description))));

        return Result.Success();
    }
}
