using MediatR;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Application.Abstractions.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Products
{
    /// <summary>
    /// Represents a query to get the product list.
    /// </summary>
    public class GetProductListQuery : IRequest<Result<List<ProductResponse>>>
    {
    }

    /// <summary>
    /// Response model for a product.
    /// </summary>
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Add other fields as needed
    }

    /// <summary>
    /// Handles GetProductListQuery requests.
    /// </summary>
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, Result<List<ProductResponse>>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductListQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<List<ProductResponse>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            // Map entities to response models (you could use AutoMapper for bigger projects)
            var response = new List<ProductResponse>();
            foreach (var product in products)
            {
                response.Add(new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name
                    // Map other fields as needed
                });
            }

            return response;
        }
    }
}
