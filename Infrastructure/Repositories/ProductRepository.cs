namespace Orders.Infrastructure.Repositories
{
    using Orders.Domain.Entities;
    using Orders.Domain.Interfaces;
    using Orders.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Implementación del repositorio de productos usando Entity Framework Core.
    /// Encapsula toda la lógica de acceso a datos para productos.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        public async Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        /// <summary>
        /// Obtiene todos los productos con paginación y filtro opcional.
        /// </summary>
        public async Task<(List<Product> Products, int TotalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? nameFilter = null,
            CancellationToken cancellationToken = default)
        {
            // Validar parámetros
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100; // Máximo 100 por página

            // Construir query
            var query = _context.Products.AsQueryable();

            // Aplicar filtro de nombre si existe
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                query = query.Where(p => p.Name.Contains(nameFilter));
            }

            // Contar total
            var totalCount = await query.CountAsync(cancellationToken);

            // Aplicar paginación y ordenamiento
            var products = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (products, totalCount);
        }

        /// <summary>
        /// Agrega un nuevo producto.
        /// </summary>
        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await Task.CompletedTask; // Operación síncrona, pero mantenemos la firma async
        }

        /// <summary>
        /// Elimina un producto.
        /// </summary>
        public async Task DeleteAsync(int productId, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(productId, cancellationToken);
            if (product != null)
            {
                _context.Products.Remove(product);
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
