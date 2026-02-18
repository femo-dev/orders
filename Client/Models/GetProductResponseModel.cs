namespace Client.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Modelo para respuesta paginada de productos.
    /// </summary>
    public class GetProductsResponseModel
    {
        [JsonPropertyName("products")]
        public List<ProductModel> Products { get; set; } = new();

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
