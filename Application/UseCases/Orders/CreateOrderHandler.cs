using Orders.Application.Exceptions;
using Orders.Application.DTOs;
using Orders.Application.UseCases.Orders.Interfaces;
using Orders.Domain.Entities;
using Orders.Domain.Exceptions;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Orders
{
    /// <summary>
    /// Manejador del caso de uso de creación de orden.
    /// Orquesta la lógica de aplicación, dominio e infraestructura.
    /// 
    /// Reglas de negocio validadas:
    /// 1. El producto debe existir
    /// 2. Debe haber stock suficiente
    /// 3. El precio se congela en el momento de la orden
    /// </summary>
    public class CreateOrderHandler : ICreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderHandler(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de crear una orden.
        /// </summary>
        public async Task<OrderResponse> HandleAsync(
            CreateOrderCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // 1. Validar que hay al menos un item
                if (command.Items == null || command.Items.Count == 0)
                    throw new ValidationException("La orden debe contener al menos un item.");

                // 2. Crear la orden (validar nombre en dominio)
                var order = Order.Create(command.CustomerName);

                // 3. Validar productos y crear items
                foreach (var (productId, quantity) in command.Items)
                {
                    var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

                    // 3a. Validar que el producto existe
                    if (product == null)
                        throw new NotFoundException("Producto", productId);

                    // 3b. Validar que hay stock suficiente (REGLA DE NEGOCIO CRÍTICA)
                    if (!product.HasSufficientStock(quantity))
                        throw new ValidationException(
                            $"Stock insuficiente para el producto '{product.Name}'. " +
                            $"Disponible: {product.Stock.Quantity}, Solicitado: {quantity}");

                    // 3c. Crear el item congelando el precio actual (REGLA DE NEGOCIO CRÍTICA)
                    var orderItem = OrderItem.Create(productId, quantity, product.Price.Amount);

                    // 3d. Agregar el item a la orden
                    order.AddItem(orderItem);

                    // 3e. Reducir el stock del producto
                    product.ReduceStock(quantity);

                    // 3f. Persistir el cambio de stock
                    await _productRepository.UpdateAsync(product, cancellationToken);
                }

                // 4. Persistir la orden
                await _orderRepository.AddAsync(order, cancellationToken);
                await _orderRepository.SaveChangesAsync(cancellationToken);

                // 5. Persistir cambios de productos (stock reducido)
                await _productRepository.SaveChangesAsync(cancellationToken);

                // 6. Mapear a DTO
                return MapToOrderResponse(order);
            }
            catch (OrderDomainException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exceptions.ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exceptions.ApplicationException($"Error al crear la orden: {ex.Message}", ex);
            }
        }

        private static OrderResponse MapToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Items = order.Items.Select(item => new OrderItemResponse
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity.Value,
                    UnitPrice = item.UnitPrice.Amount,
                    Subtotal = item.GetSubtotal()
                }).ToList(),
                Total = order.GetTotal(),
                TotalItems = order.GetTotalItems(),
                CreatedAt = order.CreatedAt
            };
        }
    }
}
