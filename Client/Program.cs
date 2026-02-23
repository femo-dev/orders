using System.Text;
using Client;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
Console.WriteLine("🚀 Cliente de Sistema Productos y Órdenes\n");

var client = new ApiClient("http://localhost:5267"); //7266

// ==================== MENÚ INTERACTIVO ====================

while (true)
{
    Console.WriteLine("\n================================");
    Console.WriteLine("       MENÚ PRINCIPAL");
    Console.WriteLine("================================");
    Console.WriteLine("1. Ver todos los productos");
    Console.WriteLine("2. Crear un nuevo producto");
    Console.WriteLine("3. Actualizar un producto");
    Console.WriteLine("4. Eliminar un producto");
    Console.WriteLine("5. Ver todas las órdenes");
    Console.WriteLine("6. Ver orden por ID");
    Console.WriteLine("7. Crear una nueva orden");
    Console.WriteLine("8. Eliminar una orden");
    Console.WriteLine("9. Ejecutar flujo de prueba");
    Console.WriteLine("0. Salir");
    Console.WriteLine("================================");
    Console.Write("\nSelecciona una opción: ");

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            await VerProductos(client);
            break;

        case "2":
            await CrearProducto(client);
            break;

        case "3":
            await ActualizarProducto(client);
            break;

        case "4":
            await EliminarProducto(client);
            break;

        case "5":
            await VerOrdenes(client);
            break;

        case "6":
            await VerOrdenPorId(client);
            break;

        case "7":
            await CrearOrden(client);
            break;

        case "8":
            await EliminarOrden(client);
            break;

        case "9":
            await EjecutarFlujoPrueba(client);
            break;

        case "0":
            Console.WriteLine("\n👋 ¡Hasta luego!");
            return;

        default:
            Console.WriteLine("\n❌ Opción inválida. Intenta de nuevo.");
            break;
    }
}

// ==================== FUNCIONES ====================

async Task VerProductos(ApiClient client)
{
    Console.Write("\nNúmero de página (por defecto 1): ");
    var pageNumberStr = Console.ReadLine();
    int.TryParse(pageNumberStr, out var pageNumber);
    if (pageNumber < 1) pageNumber = 1;

    Console.Write("Tamaño de página (por defecto 10): ");
    var pageSizeStr = Console.ReadLine();
    int.TryParse(pageSizeStr, out var pageSize);
    if (pageSize < 1) pageSize = 10;

    Console.Write("Filtrar por nombre (opcional): ");
    var nameFilter = Console.ReadLine();

    Console.WriteLine("\n⏳ Obteniendo productos...");
    var result = await client.GetProductsAsync(pageNumber, pageSize, nameFilter);

    if (result == null)
        return;

    Console.WriteLine($"\n✅ Se encontraron {result.TotalCount} productos (Página {result.PageNumber} de {result.TotalPages}):\n");

    foreach (var product in result.Products)
    {
        Console.WriteLine($"  {product}");
    }
}

async Task CrearProducto(ApiClient client)
{
    Console.Write("\nNombre del producto: ");
    var name = Console.ReadLine();

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out var price))
    {
        Console.WriteLine("❌ Precio inválido.");
        return;
    }

    Console.Write("Stock: ");
    if (!int.TryParse(Console.ReadLine(), out var stock))
    {
        Console.WriteLine("❌ Stock inválido.");
        return;
    }

    Console.WriteLine("\n⏳ Creando producto...");
    var result = await client.CreateProductAsync(name, price, stock);

    if (result != null)
    {
        Console.WriteLine($"\n✅ Producto creado exitosamente:\n  {result}");
    }
}

async Task ActualizarProducto(ApiClient client)
{
    Console.Write("\nID del producto a actualizar: ");
    if (!int.TryParse(Console.ReadLine(), out var productId))
    {
        Console.WriteLine("❌ ID inválido.");
        return;
    }

    Console.Write("Nuevo nombre (dejar en blanco para no cambiar): ");
    var name = Console.ReadLine();

    Console.Write("Nuevo precio (dejar en blanco para no cambiar): ");
    decimal? price = null;
    var priceStr = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(priceStr) && decimal.TryParse(priceStr, out var p))
        price = p;

    Console.WriteLine("\n⏳ Actualizando producto...");
    var result = await client.UpdateProductAsync(productId, string.IsNullOrWhiteSpace(name) ? null : name, price);

    if (result != null)
    {
        Console.WriteLine($"\n✅ Producto actualizado:\n  {result}");
    }
}

async Task EliminarProducto(ApiClient client)
{
    Console.Write("\nID del producto a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out var productId))
    {
        Console.WriteLine("❌ ID inválido.");
        return;
    }

    Console.WriteLine("\n⏳ Eliminando producto...");
    var success = await client.DeleteProductAsync(productId);

    if (success)
    {
        Console.WriteLine($"\n✅ Producto eliminado exitosamente.");
    }
}

async Task VerOrdenes(ApiClient client)
{
    Console.Write("\nNúmero de página (por defecto 1): ");
    var pageNumberStr = Console.ReadLine();
    int.TryParse(pageNumberStr, out var pageNumber);
    if (pageNumber < 1) pageNumber = 1;

    Console.Write("Tamaño de página (por defecto 10): ");
    var pageSizeStr = Console.ReadLine();
    int.TryParse(pageSizeStr, out var pageSize);
    if (pageSize < 1) pageSize = 10;

    Console.WriteLine("\n⏳ Obteniendo órdenes...");
    var result = await client.GetOrdersAsync(pageNumber, pageSize);

    if (result == null)
        return;

    Console.WriteLine($"\n✅ Se encontraron {result.TotalCount} órdenes (Página {result.PageNumber} de {result.TotalPages}):\n");

    foreach (var order in result.Orders)
    {
        Console.WriteLine($"  {order}");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"    {item}");
        }
    }
}

async Task VerOrdenPorId(ApiClient client)
{
    Console.Write("\nID de la orden: ");
    if (!int.TryParse(Console.ReadLine(), out var orderId))
    {
        Console.WriteLine("❌ ID inválido.");
        return;
    }

    Console.WriteLine("\n⏳ Obteniendo orden...");
    var result = await client.GetOrderByIdAsync(orderId);

    if (result != null)
    {
        Console.WriteLine($"\n✅ Orden encontrada:\n  {result}");
        foreach (var item in result.Items)
        {
            Console.WriteLine($"    {item}");
        }
    }
}

async Task CrearOrden(ApiClient client)
{
    Console.Write("\nNombre del cliente: ");
    var customerName = Console.ReadLine();

    var items = new List<(int, int)>();

    Console.WriteLine("Agregar items a la orden (escribir '0' para terminar):");
    while (true)
    {
        Console.Write("  ID del producto (0 para terminar): ");
        if (!int.TryParse(Console.ReadLine(), out var productId) || productId == 0)
            break;

        Console.Write("  Cantidad: ");
        if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity <= 0)
        {
            Console.WriteLine("  ❌ Cantidad inválida.");
            continue;
        }

        items.Add((productId, quantity));
        Console.WriteLine($"  ✓ Item agregado");
    }

    if (items.Count == 0)
    {
        Console.WriteLine("\n❌ La orden debe tener al menos un item.");
        return;
    }

    Console.WriteLine("\n⏳ Creando orden...");
    var result = await client.CreateOrderAsync(customerName, items);

    if (result != null)
    {
        Console.WriteLine($"\n✅ Orden creada exitosamente:\n  {result}");
        foreach (var item in result.Items)
        {
            Console.WriteLine($"    {item}");
        }
    }
}

async Task EliminarOrden(ApiClient client)
{
    Console.Write("\nID de la orden a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out var orderId))
    {
        Console.WriteLine("❌ ID inválido.");
        return;
    }

    Console.WriteLine("\n⏳ Eliminando orden...");
    var success = await client.DeleteOrderAsync(orderId);

    if (success)
    {
        Console.WriteLine($"\n✅ Orden eliminada exitosamente (stock restaurado).");
    }
}

async Task EjecutarFlujoPrueba(ApiClient client)
{
    Console.WriteLine("\n" + new string('=', 50));
    Console.WriteLine("FLUJO DE PRUEBA AUTOMÁTICO");
    Console.WriteLine(new string('=', 50));

    // 1. Crear un producto
    Console.WriteLine("\n1️⃣  Creando un nuevo producto...");
    var product = await client.CreateProductAsync("Laptop Gaming", 1299.99m, 3);
    if (product == null) return;
    Console.WriteLine($"✅ Producto creado: {product}");

    // 2. Obtener el producto
    Console.WriteLine($"\n2️⃣  Obteniendo el producto ID {product.Id}...");
    var fetchedProduct = await client.GetProductByIdAsync(product.Id);
    if (fetchedProduct == null) return;
    Console.WriteLine($"✅ Producto obtenido: {fetchedProduct}");

    // 3. Crear una orden
    Console.WriteLine($"\n3️⃣  Creando una orden con el producto...");
    var order = await client.CreateOrderAsync("Cliente Prueba", new List<(int, int)> { (product.Id, 2) });
    if (order == null) return;
    Console.WriteLine($"✅ Orden creada: {order}");
    foreach (var item in order.Items)
    {
        Console.WriteLine($"   {item}");
    }

    // 4. Verificar stock reducido
    Console.WriteLine($"\n4️⃣  Verificando que el stock se redujo...");
    var updatedProduct = await client.GetProductByIdAsync(product.Id);
    if (updatedProduct == null) return;
    Console.WriteLine($"✅ Stock actualizado: {updatedProduct}");
    Console.WriteLine($"   Stock anterior: 3, Stock actual: {updatedProduct.Stock}");

    // 5. Obtener la orden
    Console.WriteLine($"\n5️⃣  Obteniendo la orden creada...");
    var fetchedOrder = await client.GetOrderByIdAsync(order.Id);
    if (fetchedOrder == null) return;
    Console.WriteLine($"✅ Orden obtenida: {fetchedOrder}");

    // 6. Eliminar la orden (restaurar stock)
    Console.WriteLine($"\n6️⃣  Eliminando la orden para restaurar stock...");
    var deleted = await client.DeleteOrderAsync(order.Id);
    if (!deleted) return;
    Console.WriteLine($"✅ Orden eliminada");

    // 7. Verificar stock restaurado
    Console.WriteLine($"\n7️⃣  Verificando que el stock se restauró...");
    var restoredProduct = await client.GetProductByIdAsync(product.Id);
    if (restoredProduct == null) return;
    Console.WriteLine($"✅ Stock restaurado: {restoredProduct}");
    Console.WriteLine($"   Stock anterior: {updatedProduct.Stock}, Stock actual: {restoredProduct.Stock}");

    Console.WriteLine("\n" + new string('=', 50));
    Console.WriteLine("✅ FLUJO DE PRUEBA COMPLETADO EXITOSAMENTE");
    Console.WriteLine(new string('=', 50));
}