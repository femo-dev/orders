namespace Application.UseCases.Orders.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato para el caso de uso de eliminación de órdenes.
    /// </summary>
    public interface IDeleteOrderUseCase
    {
        Task HandleAsync(
            DeleteOrderCommand command,
            CancellationToken cancellationToken = default);
    }
}
