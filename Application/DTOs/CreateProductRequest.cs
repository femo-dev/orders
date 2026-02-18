namespace Application.DTOs
{

    /// <summary>
    /// DTO para la solicitud de creación de producto.
    /// Se recibe del cliente como JSON Body.
    /// </summary>
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
