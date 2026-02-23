using Application.DTOs;

namespace Application.UseCases.Products.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del caso de uso de obtención de un producto.
    /// </summary>
    public interface IGetProductByIdUseCase
    {
        Task<ProductResponse> HandleAsync(
            GetProductByIdCommand command,
            CancellationToken cancellationToken = default);
    }
}
