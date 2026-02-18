namespace SistemaProductosOrdenes.WebApi.Controllers;

using Application.DTOs;
using Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controlador para gestionar órdenes.
/// Expone endpoints REST para operaciones CRUD de órdenes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _createOrderHandler;
    private readonly GetOrdersHandler _getOrdersHandler;
    private readonly GetOrderByIdHandler _getOrderByIdHandler;
    private readonly DeleteOrderHandler _deleteOrderHandler;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        CreateOrderHandler createOrderHandler,
        GetOrdersHandler getOrdersHandler,
        GetOrderByIdHandler getOrderByIdHandler,
        DeleteOrderHandler deleteOrderHandler,
        ILogger<OrdersController> logger)
    {
        _createOrderHandler = createOrderHandler;
        _getOrdersHandler = getOrdersHandler;
        _getOrderByIdHandler = getOrderByIdHandler;
        _deleteOrderHandler = deleteOrderHandler;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todas las órdenes con paginación.
    /// </summary>
    /// <param name="pageNumber">Número de página (por defecto 1)</param>
    /// <param name="pageSize">Cantidad de elementos por página (por defecto 10)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista paginada de órdenes</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetOrdersResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obteniendo órdenes. Página: {PageNumber}, Tamaño: {PageSize}",
            pageNumber, pageSize);

        var command = new GetOrdersCommand(pageNumber, pageSize);
        var result = await _getOrdersHandler.HandleAsync(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Obtiene una orden específica por su ID con todos sus items.
    /// </summary>
    /// <param name="id">ID de la orden</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Datos de la orden</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderById(
        int id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Obteniendo orden con ID: {OrderId}", id);

        var command = new GetOrderByIdCommand(id);
        var result = await _getOrderByIdHandler.HandleAsync(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Crea una nueva orden.
    /// 
    /// Reglas de negocio aplicadas:
    /// 1. Valida que los productos existan
    /// 2. Valida que haya stock suficiente
    /// 3. Congela el precio en el momento de la orden
    /// 4. Reduce automáticamente el stock
    /// </summary>
    /// <param name="request">Datos de la orden (customerName e items)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Orden creada con ID asignado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creando nueva orden para cliente: {CustomerName} con {ItemCount} items",
            request.CustomerName, request.Items.Count);

        var items = request.Items
            .Select(item => (item.ProductId, item.Quantity))
            .ToList();

        var command = new CreateOrderCommand(request.CustomerName, items);
        var result = await _createOrderHandler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Elimina una orden y restaura el stock de todos los productos.
    /// 
    /// Reglas de negocio aplicadas:
    /// 1. Verifica que la orden existe
    /// 2. Restaura el stock de cada producto
    /// 3. Elimina la orden
    /// </summary>
    /// <param name="id">ID de la orden a eliminar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Sin contenido</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(
        int id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Eliminando orden con ID: {OrderId}", id);

        var command = new DeleteOrderCommand(id);
        await _deleteOrderHandler.HandleAsync(command, cancellationToken);

        return NoContent();
    }
}