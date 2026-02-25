namespace Orders.Application.Exceptions
{
    /// <summary>
    /// Excepción lanzada cuando hay errores de validación.
    /// </summary>
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message) : base(message) { }
    }
}
