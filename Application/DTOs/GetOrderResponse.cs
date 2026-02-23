namespace Orders.Application.DTOs
{
    /// <summary>
    /// DTO para la respuesta paginada de órdenes.
    /// </summary>
    public class GetOrdersResponse
    {
        public List<OrderResponse> Orders { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    }
}
