namespace Application.DTOs
{
    /// <summary>
    /// DTO para la respuesta de operaciones con órdenes.
    /// </summary>
    public class OrderResponse
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<OrderItemResponse> Items { get; set; } = new();
        public decimal Total { get; set; }
        public int TotalItems { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
