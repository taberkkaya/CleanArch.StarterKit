using CleanArch.StarterKit.Application.Abstractions.Repositories;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Infrastructure.Context;
using RepositoryKit.EntityFramework.Implementations;

namespace CleanArch.StarterKit.Infrastructure.Repositories
{
    /// <summary>
    /// Concrete implementation of product repository using RepositoryKit and Entity Framework.
    /// </summary>
    public class ProductRepository : EfRepository<Product, ApplicationDbContext>, IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
