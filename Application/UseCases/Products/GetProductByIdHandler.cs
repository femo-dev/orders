using Orders.Application.Common;
using Orders.Application.DTOs;
using Orders.Application.UseCases.Products.Interfaces;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Products
{
    /// <summary>
    /// Manejador del caso de uso de obtener un producto por ID.
    /// </summary>
    public class GetProductByIdHandler : IGetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso.
        /// </summary>
        public async Task<ProductResponse> HandleAsync(
            GetProductByIdCommand command,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);

            if (product == null)
                throw new NotFoundException("Producto", command.ProductId);

            return MapToProductResponse(product);
        }

        private static ProductResponse MapToProductResponse(Domain.Entities.Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.Amount,
                Stock = product.Stock.Quantity,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
