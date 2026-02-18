namespace Application.DTOs
{
    /// <summary>
    /// DTO para la respuesta paginada de productos.
    /// </summary>
    public class GetProductsResponse
    {
        public List<ProductResponse> Products { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    }
}
