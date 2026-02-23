using Orders.Domain.Exceptions;
using Orders.Domain.ValueObjects;

namespace Orders.Domain.Entities
{
    /// <summary>
    /// Entidad Producto del dominio.
    /// Encapsula toda la lógica de negocio relacionada con productos.
    /// </summary>
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Money Price { get; private set; } = null!;
        public Stock Stock { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }

        // Constructor privado para implementar el patrón Factory
        private Product(int id, string name, Money price, Stock stock, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Factory method para crear un nuevo producto.
        /// Valida todas las reglas de negocio.
        /// </summary>
        public static Product Create(string name, decimal price, int stock)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductDomainException("El nombre del producto no puede estar vacío.");

            if (name.Length > 120)
                throw new ProductDomainException("El nombre del producto no puede exceder 120 caracteres.");

            if (price <= 0)
                throw new ProductDomainException("El precio debe ser mayor a 0.");

            if (stock < 0)
                throw new ProductDomainException("El stock no puede ser negativo.");

            return new Product(
                id: 0, // Se asigna en la base de datos
                name: name,
                price: Money.Create(price),
                stock: Stock.Create(stock),
                createdAt: DateTime.UtcNow
            );
        }

        /// <summary>
        /// Actualiza el precio del producto.
        /// </summary>
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ProductDomainException("El precio debe ser mayor a 0.");

            Price = Money.Create(newPrice);
        }

        /// <summary>
        /// Actualiza el nombre del producto.
        /// </summary>
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ProductDomainException("El nombre del producto no puede estar vacío.");

            if (newName.Length > 120)
                throw new ProductDomainException("El nombre del producto no puede exceder 120 caracteres.");

            Name = newName;
        }

        /// <summary>
        /// Reduce el stock cuando se crea una orden.
        /// </summary>
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ProductDomainException("La cantidad debe ser mayor a 0.");

            Stock = Stock.Reduce(quantity);
        }

        /// <summary>
        /// Incrementa el stock cuando se cancela una orden.
        /// </summary>
        public void RestoreStock(int quantity)
        {
            if (quantity <= 0)
                throw new ProductDomainException("La cantidad debe ser mayor a 0.");

            Stock = Stock.Increase(quantity);
        }

        /// <summary>
        /// Verifica si hay suficiente stock para una cantidad específica.
        /// </summary>
        public bool HasSufficientStock(int quantity)
        {
            return Stock.Quantity >= quantity;
        }
    }
}
