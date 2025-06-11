using CleanArch.StarterKit.Application.Abstractions.Repositories;
using CleanArch.StarterKit.Domain.Entities;
using MediatR;
using RepositoryKit.Core.Interfaces;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Products
{
    /// <summary>
    /// Command to create a new product.
    /// </summary>
    public class ProductCreateCommand : IRequest<Result<ProductResponse>>
    {
        public string Name { get; set; }
        // Add other properties as needed
    }

    /// <summary>
    /// Handles the creation of a new product.
    /// </summary>
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Result<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCreateCommandHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductResponse>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            // You can add business/validation rules here
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result<ProductResponse>.Failure(new Error("404", "Product name cannot be empty."));
            }

            var entity = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name
                // Set other fields as needed
            };

            await _productRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new ProductResponse
            {
                Id = entity.Id,
                Name = entity.Name
                // Map other fields
            };

            return Result<ProductResponse>.Success(response);
        }
    }
}
