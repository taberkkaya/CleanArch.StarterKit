using CleanArch.StarterKit.Application.Repositories;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Infrastructure.Persistence;
using RepositoryKit.EntityFramework.Implementations;

namespace CleanArch.StarterKit.Infrastructure.Repositories;

/// <summary>
/// Entity Framework repository implementation for managing Hangfire dashboard users.
/// </summary>
internal sealed class HangfireDashboardUsersRepository
    : EfRepository<HangfireDashboardUser, ApplicationDbContext>, IHangfireDashboardUsersRepository
{
    public HangfireDashboardUsersRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}
