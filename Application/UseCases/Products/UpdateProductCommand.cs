using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Products
{
    /// <summary>
    /// Comando para actualizar un producto.
    /// </summary>
    public class UpdateProductCommand
    {
        public int ProductId { get; }
        public string? Name { get; }
        public decimal? Price { get; }

        public UpdateProductCommand(int productId, string? name, decimal? price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }
}
