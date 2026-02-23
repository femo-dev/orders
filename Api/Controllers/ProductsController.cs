namespace Orders.Api.Controllers;

using Orders.Application.DTOs;
using Orders.Application.UseCases.Products;
using Orders.Application.UseCases.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador para gestionar productos.
/// Expone endpoints REST para operaciones CRUD de productos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly ICreateProductUseCase _createProductUseCase;
    private readonly IUpdateProductUseCase _updateProductUseCase;
    private readonly IGetProductsUseCase _getProductsUseCase;
    private readonly IGetProductByIdUseCase _getProductByIdUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        ICreateProductUseCase createProductUseCase,
        IUpdateProductUseCase updateProductUseCase,
        IGetProductsUseCase getProductsUseCase,
        IGetProductByIdUseCase getProductByIdUseCase,
        IDeleteProductUseCase deleteProductUseCase,
        ILogger<ProductsController> logger)
    {
        _createProductUseCase = createProductUseCase;
        _updateProductUseCase = updateProductUseCase;
        _getProductsUseCase = getProductsUseCase;
        _getProductByIdUseCase = getProductByIdUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los productos con paginación y filtro opcional.
    /// </summary>
    /// <param name="pageNumber">Número de página (por defecto 1)</param>
    /// <param name="pageSize">Cantidad de elementos por página (por defecto 10)</param>
    /// <param name="nameFilter">Filtro opcional por nombre</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista paginada de productos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? nameFilter = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obteniendo productos. Página: {PageNumber}, Tamaño: {PageSize}, Filtro: {NameFilter}",
            pageNumber, pageSize, nameFilter);

        var command = new GetProductsCommand(pageNumber, pageSize, nameFilter);
        var result = await _getProductsUseCase.HandleAsync(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Obtiene un producto específico por su ID.
    /// </summary>
    /// <param name="id">ID del producto</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Datos del producto</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(
        int id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obteniendo producto con ID: {ProductId}", id);

        var command = new GetProductByIdCommand(id);
        var result = await _getProductByIdUseCase.HandleAsync(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    /// <param name="request">Datos del producto a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Producto creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creando nuevo producto: {ProductName}", request.Name);

        var command = new CreateProductCommand(request.Name, request.Price, request.Stock);
        var result = await _createProductUseCase.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Actualiza un producto existente.
    /// </summary>
    /// <param name="id">ID del producto a actualizar</param>
    /// <param name="request">Datos a actualizar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Producto actualizado</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct(
        int id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Actualizando producto con ID: {ProductId}", id);

        var command = new UpdateProductCommand(id, request.Name, request.Price);
        var result = await _updateProductUseCase.HandleAsync(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Elimina un producto.
    /// </summary>
    /// <param name="id">ID del producto a eliminar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Sin contenido</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(
        int id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Eliminando producto con ID: {ProductId}", id);

        var command = new DeleteProductCommand(id);
        await _deleteProductUseCase.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}