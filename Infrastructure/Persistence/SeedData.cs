namespace Orders.Infrastructure.Persistence
{
    using Orders.Domain.Entities;

    /// <summary>
    /// Clase auxiliar para poblar la base de datos con datos iniciales.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Inicializa los datos de prueba en la base de datos.
        /// </summary>
        public static async Task InitializeSeedAsync(ApplicationDbContext context)
        {
            // Evitar duplicados
            if (context.Products.Any())
            {
                return; // Base de datos ya tiene datos
            }

            // Crear productos de prueba
            var products = new List<Product>
        {
            Product.Create("Laptop HP Pavilion", 899.99m, 5),
            Product.Create("Mouse Logitech", 29.99m, 50),
            Product.Create("Teclado Mecánico", 149.99m, 15),
            Product.Create("Monitor LG 27\"", 299.99m, 8),
            Product.Create("Webcam HD", 79.99m, 20),
            Product.Create("Audífonos Sony", 199.99m, 12),
            Product.Create("Cable USB-C", 15.99m, 100),
            Product.Create("Mousepad Gaming", 24.99m, 30),
        };

            // Agregar a la base de datos
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
