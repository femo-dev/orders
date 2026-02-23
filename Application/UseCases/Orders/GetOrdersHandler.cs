using Orders.Application.DTOs;
using Orders.Application.UseCases.Orders.Interfaces;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Orders
{
    /// <summary>
    /// Manejador del caso de uso de obtener órdenes.
    /// </summary>
    public class GetOrdersHandler : IGetOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de obtener órdenes.
        /// </summary>
        public async Task<GetOrdersResponse> HandleAsync(
            GetOrdersCommand command,
            CancellationToken cancellationToken = default)
        {
            var (orders, totalCount) = await _orderRepository.GetAllAsync(
                pageNumber: command.PageNumber,
                pageSize: command.PageSize,
                cancellationToken: cancellationToken);

            var orderResponses = orders.Select(MapToOrderResponse).ToList();

            return new GetOrdersResponse
            {
                Orders = orderResponses,
                PageNumber = command.PageNumber,
                PageSize = command.PageSize,
                TotalCount = totalCount
            };
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
