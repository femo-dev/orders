namespace Orders.Application.UseCases.Orders
{

    /// <summary>
    /// Comando para crear una orden.
    /// </summary>
    public class CreateOrderCommand
    {
        public string CustomerName { get; }
        public List<(int ProductId, int Quantity)> Items { get; }

        public CreateOrderCommand(string customerName, List<(int ProductId, int Quantity)> items)
        {
            CustomerName = customerName;
            Items = items;
        }
    }
}
