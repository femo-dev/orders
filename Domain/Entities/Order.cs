using Orders.Domain.Exceptions;

namespace Orders.Domain.Entities
{
    /// <summary>
    /// Entidad Orden del dominio.
    /// Agregado raíz que encapsula la lógica de órdenes.
    /// </summary>
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public List<OrderItem> Items { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        // Constructor privado
        private Order(int id, string customerName, DateTime createdAt)
        {
            Id = id;
            CustomerName = customerName;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Factory method para crear una nueva orden.
        /// </summary>
        public static Order Create(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new OrderDomainException("El nombre del cliente no puede estar vacío.");

            if (customerName.Length > 120)
                throw new OrderDomainException("El nombre del cliente no puede exceder 120 caracteres.");

            return new Order(
                id: 0, // Se asigna en la base de datos
                customerName: customerName,
                createdAt: DateTime.UtcNow
            );
        }

        /// <summary>
        /// Agrega un item a la orden.
        /// </summary>
        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new OrderDomainException("El item de orden no puede ser nulo.");

            // Verificar que no exista el mismo producto
            if (Items.Any(i => i.ProductId == item.ProductId))
                throw new OrderDomainException(
                    $"El producto con ID {item.ProductId} ya está en esta orden.");

            Items.Add(item);
        }

        /// <summary>
        /// Obtiene el total de la orden.
        /// </summary>
        public decimal GetTotal()
        {
            return Items.Sum(item => item.GetSubtotal());
        }

        /// <summary>
        /// Obtiene la cantidad total de items en la orden.
        /// </summary>
        public int GetTotalItems()
        {
            return Items.Sum(item => item.Quantity.Value);
        }

        /// <summary>
        /// Limpia los items de la orden (para cancelación).
        /// </summary>
        public void ClearItems()
        {
            Items.Clear();
        }
    }
}
