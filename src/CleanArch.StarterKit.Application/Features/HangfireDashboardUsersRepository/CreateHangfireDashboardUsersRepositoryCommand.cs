using CleanArch.StarterKit.Application.Repositories;
using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities;
using Mapster;
using MediatR;
using RepositoryKit.Core.Interfaces;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.HangfireDashboardUsersRepository;
public sealed class CreateHangfireDashboardUsersRepositoryCommand : IRequest<Result<string>>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

internal sealed class CreateHangfireDashboardUsersRepositoryCommandHandler(
    IHangfireDashboardUsersRepository dashboardUsersRepository,
    IDashboardPasswordHasher hasher,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateHangfireDashboardUsersRepositoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateHangfireDashboardUsersRepositoryCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = hasher.HashPassword(request.Password);
        var dashboardUser = request.Adapt<HangfireDashboardUser>();
        dashboardUser.PasswordHash = hashedPassword;

        await dashboardUsersRepository.AddAsync(dashboardUser,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Hangfire admin eklendi.";
    }
}
