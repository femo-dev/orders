using System.Text.Json.Serialization;

namespace Orders.Client.Models
{
    /// <summary>
    /// Modelo de producto para desérialización desde la API.
    /// </summary>
    public class ProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name} - Precio: ${Price:F2} - Stock: {Stock}";
        }
    }
}
