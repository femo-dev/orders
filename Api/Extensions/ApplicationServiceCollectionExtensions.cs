using Orders.Application.UseCases.Orders;
using Orders.Application.UseCases.Products;

namespace Orders.Api.Extensions
{
    /// <summary>
    /// Extensión para registrar servicios de la capa Application.
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Registra todos los handlers de casos de uso.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Handlers de Productos
            services.AddScoped<CreateProductHandler>();
            services.AddScoped<UpdateProductHandler>();
            services.AddScoped<GetProductsHandler>();
            services.AddScoped<GetProductByIdHandler>();
            services.AddScoped<DeleteProductHandler>();

            // Handlers de Órdenes
            services.AddScoped<CreateOrderHandler>();
            services.AddScoped<GetOrdersHandler>();
            services.AddScoped<GetOrderByIdHandler>();
            services.AddScoped<DeleteOrderHandler>();

            return services;
        }
    }
}
