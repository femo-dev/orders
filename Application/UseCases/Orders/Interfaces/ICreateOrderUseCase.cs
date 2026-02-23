using Orders.Application.DTOs;

namespace Orders.Application.UseCases.Orders.Interfaces
{
    /// <summary>
    /// Interfaz que efine el contrato para el caso de uso de creación de órdenes.
    /// </summary>
    public interface ICreateOrderUseCase
    {
        Task<OrderResponse> HandleAsync(
            CreateOrderCommand command,
            CancellationToken cancellationToken = default);
    }
}
