namespace Orders.Application.UseCases.Orders
{

    /// <summary>
    /// Comando para obtener una orden por ID.
    /// </summary>
    public class GetOrderByIdCommand
    {
        public int OrderId { get; }

        public GetOrderByIdCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
