using Orders.Application.Common;
using Orders.Application.DTOs;
using Orders.Application.Exceptions;
using Orders.Application.UseCases.Products.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Products
{
    /// <summary>
    /// Manejador del caso de uso de creación de producto.
    /// Orquesta la lógica de aplicación y coordina con el dominio.
    /// </summary>
    public class CreateProductHandler : ICreateProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de crear un producto.
        /// </summary>
        public async Task<ProductResponse> HandleAsync(
            CreateProductCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Crear el producto (lógica de dominio valida automáticamente)
                var product = Product.Create(command.Name, command.Price, command.Stock);

                // Persistir
                await _productRepository.AddAsync(product, cancellationToken);
                await _productRepository.SaveChangesAsync(cancellationToken);

                // Mapear a DTO
                return MapToProductResponse(product);
            }
            catch (ProductDomainException ex)
            {
                throw new ValidationException(ex.Message);
            }
        }

        private static ProductResponse MapToProductResponse(Product product)
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
