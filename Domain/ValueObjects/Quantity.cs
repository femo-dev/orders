namespace Orders.Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa una cantidad.
    /// Encapsula la validación de cantidades positivas.
    /// </summary>
    public class Quantity : IEquatable<Quantity>
    {
        public int Value { get; }

        private Quantity(int value)
        {
            if (value <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.", nameof(value));

            Value = value;
        }

        /// <summary>
        /// Factory method para crear una Quantity válida.
        /// </summary>
        public static Quantity Create(int value)
        {
            return new Quantity(value);
        }

        public bool Equals(Quantity? other)
        {
            return other is not null && Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Quantity);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
