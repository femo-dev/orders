namespace Orders.Application.Common
{

    /// <summary>
    /// Excepción base para errores de aplicación.
    /// </summary>
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
        public ApplicationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
