namespace Orders.Application.Common
{

    /// <summary>
    /// Excepción lanzada cuando un recurso no es encontrado.
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string resourceName, int id)
            : base($"{resourceName} con ID {id} no fue encontrado.") { }

        public NotFoundException(string message) : base(message) { }
    }
}
