namespace Orders.Application.UseCases.Products
{

    /// <summary>
    /// Comando para eliminar un producto.
    /// </summary>
    public class DeleteProductCommand
    {
        public int ProductId { get; }

        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
