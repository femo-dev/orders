using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    /// <summary>
    /// Excepción lanzada cuando se viola una regla de negocio en el dominio de Productos.
    /// </summary>
    public class ProductDomainException : Exception
    {
        public ProductDomainException(string message) : base(message) { }
    }
}
