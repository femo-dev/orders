using System.Text.Json.Serialization;

namespace Client.Models
{
    /// <summary>
    /// Modelo para respuesta paginada de órdenes.
    /// </summary>
    public class GetOrdersResponseModel
    {
        [JsonPropertyName("orders")]
        public List<OrderModel> Orders { get; set; } = new();

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }
}
