using Orders.Application.DTOs;
using Orders.Application.UseCases.Products.Interfaces;
using Orders.Domain.Interfaces;

namespace Orders.Application.UseCases.Products
{

    /// <summary>
    /// Manejador del caso de uso de actualización de producto.
    /// </summary>
    public class GetProductsHandler: IGetProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Ejecuta el caso de uso de obtener productos.
        /// </summary>
        public async Task<GetProductsResponse> HandleAsync(
            GetProductsCommand command,
            CancellationToken cancellationToken = default)
        {
            var (products, totalCount) = await _productRepository.GetAllAsync(
                pageNumber: command.PageNumber,
                pageSize: command.PageSize,
                nameFilter: command.NameFilter,
                cancellationToken: cancellationToken);

            var productResponses = products.Select(MapToProductResponse).ToList();

            return new GetProductsResponse
            {
                Products = productResponses,
                PageNumber = command.PageNumber,
                PageSize = command.PageSize,
                TotalCount = totalCount
            };
        }

        private static ProductResponse MapToProductResponse(Domain.Entities.Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price.Amount,
                Stock = product.Stock.Quantity,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
