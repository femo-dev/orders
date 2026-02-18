using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Contrato para el repositorio de productos.
    /// Define operaciones de persistencia sin revelar detalles de implementación.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene todos los productos con paginación y filtro opcional.
        /// </summary>
        Task<(List<Product> Products, int TotalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? nameFilter = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        Task AddAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina un producto.
        /// </summary>
        Task DeleteAsync(int productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Guarda los cambios pendientes.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
