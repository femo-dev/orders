using Domain.Interfaces;
using Application.Common;
using Application.DTOs;
using Domain.Exceptions;
using Application.UseCases.Products.Interfaces;

namespace Application.UseCases.Products
{
    /// <summary>
    /// Manejador del caso de uso de actualización de producto.
    /// </summary>
    public class UpdateProductHandler : IUpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de actualizar un producto.
        /// </summary>
        public async Task<ProductResponse> HandleAsync(
            UpdateProductCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Obtener el producto
                var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);
                if (product == null)
                    throw new NotFoundException("Producto", command.ProductId);

                // Actualizar campos si fueron proporcionados
                if (!string.IsNullOrWhiteSpace(command.Name))
                    product.UpdateName(command.Name);

                if (command.Price.HasValue && command.Price > 0)
                    product.UpdatePrice(command.Price.Value);

                // Persistir cambios
                await _productRepository.UpdateAsync(product, cancellationToken);
                await _productRepository.SaveChangesAsync(cancellationToken);

                // Mapear a DTO
                return MapToProductResponse(product);
            }
            catch (ProductDomainException ex)
            {
                throw new ValidationException(ex.Message);
            }
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
