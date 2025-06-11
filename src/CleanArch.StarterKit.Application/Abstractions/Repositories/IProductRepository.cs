using CleanArch.StarterKit.Domain.Entities;
using RepositoryKit.Core.Interfaces;

namespace CleanArch.StarterKit.Application.Abstractions.Repositories
{
    /// <summary>
    /// Abstraction for product repository operations using RepositoryKit.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        // Additional custom methods for Product repository can be defined here.
    }
}
