using CleanArch.StarterKit.Application.Repositories;
using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities;
using Mapster;
using MediatR;
using RepositoryKit.Core.Interfaces;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.HangfireDashboardUsersRepository;

/// <summary>
/// Command to create a new Hangfire dashboard user with the specified username and password.
/// </summary>
public sealed class CreateUserRepositoryCommand : IRequest<Result<string>>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

/// <summary>
/// Handler that processes the creation of a new Hangfire dashboard user.
/// It hashes the password, maps the command data to the entity,
/// saves the entity to the repository, and commits the transaction.
/// </summary>
internal sealed class CreateHangfireDashboardUsersRepositoryCommandHandler(
    IHangfireDashboardUsersRepository dashboardUsersRepository,
    IPasswordHasher hasher,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateUserRepositoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserRepositoryCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = hasher.HashPassword(request.Password);
        var dashboardUser = request.Adapt<HangFireUser>();
        dashboardUser.PasswordHash = hashedPassword;

        await dashboardUsersRepository.AddAsync(dashboardUser, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Hangfire dashboard user was added successfully.";
    }
}
