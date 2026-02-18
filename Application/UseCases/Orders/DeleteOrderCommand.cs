namespace Application.UseCases.Orders
{

    /// <summary>
    /// Comando para eliminar una orden y restaurar el stock.
    /// </summary>
    public class DeleteOrderCommand
    {
        public int OrderId { get; }

        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
