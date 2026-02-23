using Application.DTOs;

namespace Application.UseCases.Products.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del caso de uso de creación de productos.
    /// </summary>
    public interface ICreateProductUseCase
    {
        Task<ProductResponse> HandleAsync(
            CreateProductCommand command,
            CancellationToken cancellationToken = default);
    }
}
