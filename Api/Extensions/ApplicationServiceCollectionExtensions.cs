using Orders.Application.UseCases.Orders;
using Orders.Application.UseCases.Orders.Interfaces;
using Orders.Application.UseCases.Products;
using Orders.Application.UseCases.Products.Interfaces;

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
            services.AddScoped<ICreateProductUseCase, CreateProductHandler>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductHandler>();
            services.AddScoped<IGetProductsUseCase, GetProductsHandler>();
            services.AddScoped<IGetProductByIdUseCase, GetProductByIdHandler>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductHandler>();

            // Handlers de Órdenes
            services.AddScoped<ICreateOrderUseCase, CreateOrderHandler>();
            services.AddScoped<IGetOrdersUseCase, GetOrdersHandler>();
            services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdHandler>();
            services.AddScoped<IDeleteOrderUseCase, DeleteOrderHandler>();

            return services;
        }
    }
}
