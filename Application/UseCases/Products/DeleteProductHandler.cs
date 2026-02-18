using Application.Common;
using Domain.Interfaces;

namespace Application.UseCases.Products
{
    /// <summary>
    /// Manejador del caso de uso de eliminar un producto.
    /// </summary>
    public class DeleteProductHandler
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
