namespace Application.Exceptions
{
    public class ProductDomainException : Exception
    {
        public ProductDomainException() : base() { }

        public ProductDomainException(string message) : base(message) { }

        public ProductDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
