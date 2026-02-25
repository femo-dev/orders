using Orders.Application.DTOs;
using Orders.Application.Exceptions;
using Orders.Application.UseCases.Orders.Interfaces;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Orders
{
    /// <summary>
    /// Manejador del caso de uso de obtener una orden por ID.
    /// </summary>
    public class GetOrderByIdHandler : IGetOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso.
        /// </summary>
        public async Task<OrderResponse> HandleAsync(
            GetOrderByIdCommand command,
            CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

            if (order == null)
                throw new NotFoundException("Orden", command.OrderId);

            return MapToOrderResponse(order);
        }

        private static OrderResponse MapToOrderResponse(Domain.Entities.Order order)
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
