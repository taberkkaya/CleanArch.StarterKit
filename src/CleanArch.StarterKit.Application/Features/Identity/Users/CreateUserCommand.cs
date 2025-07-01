using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Users;
public sealed record CreateUserCommand(
    string Email,
    string UserName,
    string Password
    ) : IRequest<Result<string>>;

internal sealed class CreateUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    ICacheService cacheService,
    IEmailService emailService
    ) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async  Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<ApplicationUser>();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<string>.ValidationFailure(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));

        await emailService.SendAsync("user@yourdomain.com","Yeni Kullanıcı",$"Yeni Kullanıcı eklendi{user.UserName}");

        cacheService.Remove("users");

        return "User registration completed successfully.";
    }
}
