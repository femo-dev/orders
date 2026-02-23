using System.Net.Http.Json;
using Orders.Client.Models;

namespace Client
{
    /// <summary>
    /// Cliente HTTP para comunicarse con la API REST.
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
        }

        // ==================== PRODUCTOS ====================

        /// <summary>
        /// Obtiene todos los productos con paginación.
        /// </summary>
        public async Task<GetProductsResponseModel?> GetProductsAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string? nameFilter = null)
        {
            try
            {
                var url = $"{_baseUrl}/api/products?pageNumber={pageNumber}&pageSize={pageSize}";

                if (!string.IsNullOrWhiteSpace(nameFilter))
                    url += $"&nameFilter={Uri.EscapeDataString(nameFilter)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<GetProductsResponseModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener productos: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un producto específico por ID.
        /// </summary>
        public async Task<ProductModel?> GetProductByIdAsync(int productId)
        {
            try
            {
                var url = $"{_baseUrl}/api/products/{productId}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener producto: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        public async Task<ProductModel?> CreateProductAsync(string name, decimal price, int stock)
        {
            try
            {
                var url = $"{_baseUrl}/api/products";
                var request = new { name, price, stock };

                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error: {response.StatusCode} - {errorContent}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear producto: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        public async Task<ProductModel?> UpdateProductAsync(int productId, string? name, decimal? price)
        {
            try
            {
                var url = $"{_baseUrl}/api/products/{productId}";
                var request = new { name, price };

                var response = await _httpClient.PutAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error: {response.StatusCode} - {errorContent}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar producto: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Elimina un producto.
        /// </summary>
        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var url = $"{_baseUrl}/api/products/{productId}";
                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar producto: {ex.Message}");
                return false;
            }
        }

        // ==================== ÓRDENES ====================

        /// <summary>
        /// Obtiene todas las órdenes con paginación.
        /// </summary>
        public async Task<GetOrdersResponseModel?> GetOrdersAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var url = $"{_baseUrl}/api/orders?pageNumber={pageNumber}&pageSize={pageSize}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<GetOrdersResponseModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener órdenes: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene una orden específica por ID.
        /// </summary>
        public async Task<OrderModel?> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var url = $"{_baseUrl}/api/orders/{orderId}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<OrderModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener orden: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crea una nueva orden.
        /// </summary>
        public async Task<OrderModel?> CreateOrderAsync(string customerName, List<(int productId, int quantity)> items)
        {
            try
            {
                var url = $"{_baseUrl}/api/orders";

                var orderItems = items.Select(i => new { productId = i.productId, quantity = i.quantity }).ToList();
                var request = new { customerName, items = orderItems };

                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ Error: {response.StatusCode} - {errorContent}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<OrderModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear orden: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Elimina una orden y restaura el stock.
        /// </summary>
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            try
            {
                var url = $"{_baseUrl}/api/orders/{orderId}";
                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Error: {response.StatusCode}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar orden: {ex.Message}");
                return false;
            }
        }
    }
}
