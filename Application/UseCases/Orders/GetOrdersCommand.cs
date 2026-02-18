namespace Application.UseCases.Orders
{

    /// <summary>
    /// Comando para obtener órdenes con paginación.
    /// </summary>
    public class GetOrdersCommand
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetOrdersCommand(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
