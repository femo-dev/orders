using Application.DTOs;

namespace Application.UseCases.Products.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del caso de uso de actualización de productos.
    /// </summary>
    public interface IUpdateProductUseCase
    {
        Task<ProductResponse> HandleAsync(
            UpdateProductCommand command,
            CancellationToken cancellationToken = default);
    }
}
