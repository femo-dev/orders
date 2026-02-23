namespace Orders.Application.UseCases.Products
{

    /// <summary>
    /// Comando para crear un producto.
    /// Encapsula los parámetros del caso de uso.
    /// </summary>
    public class CreateProductCommand
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }

        public CreateProductCommand(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
