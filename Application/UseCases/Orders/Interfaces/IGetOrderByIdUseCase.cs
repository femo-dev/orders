using Application.DTOs;

namespace Application.UseCases.Orders.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato para el caso de uso de obtención de una orden por ID.
    /// </summary>
    public interface IGetOrderByIdUseCase
    {
        Task<OrderResponse> HandleAsync(
            GetOrderByIdCommand command,
            CancellationToken cancellationToken = default);
    }
}
