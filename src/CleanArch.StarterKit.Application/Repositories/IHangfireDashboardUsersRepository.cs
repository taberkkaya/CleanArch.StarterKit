using CleanArch.StarterKit.Domain.Entities;
using RepositoryKit.Core.Interfaces;

namespace CleanArch.StarterKit.Application.Repositories;

/// <summary>
/// Repository interface for managing Hangfire dashboard user entities.
/// </summary>
public interface IHangfireDashboardUsersRepository : IRepository<HangfireDashboardUser>
{
}
