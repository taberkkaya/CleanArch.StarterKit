using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.DomainEvents.Users;
using CleanArch.StarterKit.Domain.Entities.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Identity.Users
{
    /// <summary>
    /// Command to create a new user with email, username, and password.
    /// </summary>
    public sealed record CreateUserCommand(
        string Email,
        string UserName,
        string Password
    ) : IRequest<Result<string>>;

    internal sealed class CreateUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        ICacheService cacheService,
        IMediator mediator
    ) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<ApplicationUser>();

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Result<string>.ValidationFailure(result.Errors.Select(e => new ValidationError(e.Code, e.Description)));

            await mediator.Publish(new UserCreatedEvent(user), cancellationToken);

            cacheService.Remove("users");

            return "User registration completed successfully.";
        }
    }
}
