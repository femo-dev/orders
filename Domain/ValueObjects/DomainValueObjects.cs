using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Value Object que representa un monto de dinero.
    /// Encapsula la lógica de validación del precio.
    /// </summary>
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; }

        private Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("El monto no puede ser negativo.", nameof(amount));

            Amount = decimal.Round(amount, 2);
        }

        /// <summary>
        /// Factory method para crear un Money válido.
        /// </summary>
        public static Money Create(decimal amount)
        {
            return new Money(amount);
        }

        /// <summary>
        /// Obtiene el valor formateado como moneda.
        /// </summary>
        public string GetFormatted() => Amount.ToString("C");

        public bool Equals(Money? other)
        {
            return other is not null && Amount == other.Amount;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Money);
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }

        public override string ToString()
        {
            return GetFormatted();
        }
    }
}