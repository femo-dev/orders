using Application.DTOs;

namespace Application.UseCases.Orders.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato para el caso de uso de obtención de órdenes.
    /// </summary>
    public interface IGetOrdersUseCase
    {
        Task<GetOrdersResponse> HandleAsync(
            GetOrdersCommand command,
            CancellationToken cancellationToken = default);
    }
}
