using Application.DTOs;

namespace Application.UseCases.Products.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del caso de uso de obtención de productos.
    /// </summary>
    public interface IGetProductsUseCase
    {
        Task<GetProductsResponse> HandleAsync(
            GetProductsCommand command,
            CancellationToken cancellationToken = default);
    }
}
