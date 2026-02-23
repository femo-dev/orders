using Application.Common;
using Application.UseCases.Orders.Interfaces;
using Domain.Interfaces;

namespace Application.UseCases.Orders
{
    /// <summary>
    /// Manejador del caso de uso de eliminar una orden.
    /// 
    /// Responsabilidades:
    /// 1. Verificar que la orden existe
    /// 2. Restaurar el stock de todos los productos
    /// 3. Eliminar la orden
    /// </summary>
    public class DeleteOrderHandler : IDeleteOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public DeleteOrderHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de eliminar una orden.
        /// </summary>
        public async Task HandleAsync(
            DeleteOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // 1. Obtener la orden
                var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

                if (order == null)
                    throw new NotFoundException("Orden", command.OrderId);

                // 2. Restaurar el stock de cada producto (REGLA DE NEGOCIO CRÍTICA)
                foreach (var item in order.Items)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);

                    if (product != null)
                    {
                        product.RestoreStock(item.Quantity.Value);
                        await _productRepository.UpdateAsync(product, cancellationToken);
                    }
                }

                // 3. Eliminar la orden
                await _orderRepository.DeleteAsync(command.OrderId, cancellationToken);

                // 4. Persistir cambios
                await _productRepository.SaveChangesAsync(cancellationToken);
                await _orderRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Common.ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Common.ApplicationException($"Error al eliminar la orden: {ex.Message}", ex);
            }
        }
    }
}
