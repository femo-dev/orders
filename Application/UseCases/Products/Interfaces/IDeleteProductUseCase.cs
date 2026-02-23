namespace Application.UseCases.Products.Interfaces
{
    /// <summary>
    /// Interfaz que define el contrato del caso de uso de eliminación de productos.
    /// </summary>
    public interface IDeleteProductUseCase
    {
        Task HandleAsync(
            DeleteProductCommand command,
            CancellationToken cancellationToken = default);
    }
}
