using Orders.Application.Exceptions;
using Orders.Application.UseCases.Products.Interfaces;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Products
{
    /// <summary>
    /// Manejador del caso de uso de eliminar un producto.
    /// </summary>
    public class DeleteProductHandler : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de eliminar un producto.
        /// </summary>
        public async Task HandleAsync(
            DeleteProductCommand command,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);

            if (product == null)
                throw new NotFoundException("Producto", command.ProductId);

            await _productRepository.DeleteAsync(command.ProductId, cancellationToken);
            await _productRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
