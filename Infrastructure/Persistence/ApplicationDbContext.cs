using Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Orders.Infrastructure.Persistence
{
    /// <summary>
    /// DbContext principal de la aplicación.
    /// Configura las entidades y sus relaciones en Entity Framework Core.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        /// <summary>
        /// Configura el modelo de datos.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones de entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
