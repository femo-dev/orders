namespace Application.UseCases.Products
{
    /// <summary>
    /// Comando para obtener productos con paginación y filtrado.
    /// </summary>
    public class GetProductsCommand
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? NameFilter { get; }

        public GetProductsCommand(int pageNumber = 1, int pageSize = 10, string? nameFilter = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            NameFilter = nameFilter;
        }
    }
}
