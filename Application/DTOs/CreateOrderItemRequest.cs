namespace Application.DTOs
{
    /// <summary>
    /// DTO para un item dentro de la solicitud de creación de orden.
    /// </summary>
    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
