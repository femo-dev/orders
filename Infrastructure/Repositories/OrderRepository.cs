namespace Infrastructure.Repositories
{
    using Domain.Entities;
    using Domain.Interfaces;
    using Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implementación del repositorio de órdenes usando Entity Framework Core.
    /// Encapsula toda la lógica de acceso a datos para órdenes.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene una orden por su ID con sus items incluidos (Eager Loading).
        /// </summary>
        public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Items) // Incluir items para evitar lazy loading issues
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }

        /// <summary>
        /// Obtiene todas las órdenes con paginación.
        /// </summary>
        public async Task<(List<Order> Orders, int TotalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            // Validar parámetros
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100; // Máximo 100 por página

            // Contar total
            var totalCount = await _context.Orders.CountAsync(cancellationToken);

            // Obtener órdenes con paginación
            var orders = await _context.Orders
                .Include(o => o.Items) // Incluir items
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (orders, totalCount);
        }

        /// <summary>
        /// Agrega una nueva orden.
        /// </summary>
        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        /// <summary>
        /// Elimina una orden.
        /// </summary>
        public async Task DeleteAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var order = await GetByIdAsync(orderId, cancellationToken);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        /// <summary>
        /// Guarda los cambios pendientes en la base de datos.
        /// </summary>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
