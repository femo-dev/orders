using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Extensión para registrar servicios de la capa Infrastructure.
    /// </summary>
    public static class InfrastructureServiceCollectionExtensions
    {
        /// <summary>
        /// Registra los servicios de Infrastructure en el contenedor de DI.
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            string connectionString)
        {
            // Registrar DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("SistemaProductosOrdenesDb"));

            // Registrar Repositorios
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
