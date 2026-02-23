namespace Orders.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa el stock de un producto.
    /// </summary>
    public class Stock : IEquatable<Stock>
    {
        public int Quantity { get; }

        private Stock(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("El stock no puede ser negativo.", nameof(quantity));

            Quantity = quantity;
        }

        /// <summary>
        /// Factory method para crear Stock válido.
        /// </summary>
        public static Stock Create(int quantity)
        {
            return new Stock(quantity);
        }

        /// <summary>
        /// Reduce el stock en una cantidad específica.
        /// </summary>
        public Stock Reduce(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("La cantidad a reducir debe ser mayor a 0.", nameof(amount));

            if (Quantity - amount < 0)
                throw new InvalidOperationException(
                    $"Stock insuficiente. Disponible: {Quantity}, Solicitado: {amount}");

            return new Stock(Quantity - amount);
        }

        /// <summary>
        /// Incrementa el stock en una cantidad específica.
        /// </summary>
        public Stock Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("La cantidad a incrementar debe ser mayor a 0.", nameof(amount));

            return new Stock(Quantity + amount);
        }

        public bool Equals(Stock? other)
        {
            return other is not null && Quantity == other.Quantity;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Stock);
        }

        public override int GetHashCode()
        {
            return Quantity.GetHashCode();
        }

        public override string ToString()
        {
            return Quantity.ToString();
        }
    }
}
