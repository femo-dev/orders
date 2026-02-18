using Domain.Entities;

namespace Domain.Interfaces
{

    /// <summary>
    /// Contrato para el repositorio de órdenes.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Obtiene una orden por su ID con sus items incluidos.
        /// </summary>
        Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene todas las órdenes con paginación.
        /// </summary>
        Task<(List<Order> Orders, int TotalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega una nueva orden.
        /// </summary>
        Task AddAsync(Order order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina una orden y restaura el stock de sus productos.
        /// </summary>
        Task DeleteAsync(int orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Guarda los cambios pendientes.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
