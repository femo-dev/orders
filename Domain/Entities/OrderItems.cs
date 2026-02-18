using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad que representa un item dentro de una orden.
    /// </summary>
    public class OrderItem
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public Quantity Quantity { get; private set; } = null!;
        public Money UnitPrice { get; private set; } = null!; // Precio en el momento de la orden
        public DateTime CreatedAt { get; private set; }

        // Navegación
        public Order Order { get; set; } = null!;

        // Constructor privado
        private OrderItem(int id, int orderId, int productId, Quantity quantity, Money unitPrice, DateTime createdAt)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Factory method para crear un OrderItem.
        /// </summary>
        public static OrderItem Create(int productId, int quantity, decimal unitPrice)
        {
            if (productId <= 0)
                throw new OrderDomainException("El ID del producto debe ser mayor a 0.");

            if (quantity <= 0)
                throw new OrderDomainException("La cantidad debe ser mayor a 0.");

            if (unitPrice <= 0)
                throw new OrderDomainException("El precio unitario debe ser mayor a 0.");

            return new OrderItem(
                id: 0,
                orderId: 0,
                productId: productId,
                quantity: Quantity.Create(quantity),
                unitPrice: Money.Create(unitPrice),
                createdAt: DateTime.UtcNow
            );
        }

        /// <summary>
        /// Calcula el subtotal del item (cantidad × precio unitario).
        /// </summary>
        public decimal GetSubtotal()
        {
            return Quantity.Value * UnitPrice.Amount;
        }
    }
}
