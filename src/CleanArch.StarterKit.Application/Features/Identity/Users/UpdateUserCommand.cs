using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Users;
public sealed record UpdateUserCommand(
    Guid Id,
    string Email,
    string? Password
    ) : IRequest<Result<string>>;

internal sealed class UpdateUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService
    ) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
            return Result<string>.Failure(new Error(ErrorCodes.NotFound, "User not found!"));

        if (!string.IsNullOrEmpty(request.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user,token,request.Password);
        }

        request.Adapt(user);
        
        await userManager.UpdateAsync(user);

        cacheService.Remove("users");

        return "User updated done!";
    }
}
