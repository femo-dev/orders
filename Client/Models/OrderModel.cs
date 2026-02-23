using System.Text.Json.Serialization;

namespace Orders.Client.Models
{
    /// <summary>
    /// Modelo de orden para desérialización desde la API.
    /// </summary>
    public class OrderModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<OrderItemModel> Items { get; set; } = new();

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"[Orden #{Id}] Cliente: {CustomerName} - Total: ${Total:F2} - Items: {TotalItems}";
        }
    }

    /// <summary>
    /// Modelo para items de una orden.
    /// </summary>
    public class OrderItemModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        public override string ToString()
        {
            return $"  Producto ID: {ProductId} - Cantidad: {Quantity} - Precio: ${UnitPrice:F2} - Subtotal: ${Subtotal:F2}";
        }
    }
}
