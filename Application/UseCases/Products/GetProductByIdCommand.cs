namespace Orders.Application.UseCases.Products
{

    /// <summary>
    /// Comando para obtener un producto por ID.
    /// </summary>
    public class GetProductByIdCommand
    {
        public int ProductId { get; }

        public GetProductByIdCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
