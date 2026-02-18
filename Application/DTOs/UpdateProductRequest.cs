namespace Application.DTOs
{
    /// <summary>
    /// DTO para la solicitud de actualización de producto.
    /// </summary>
    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}
