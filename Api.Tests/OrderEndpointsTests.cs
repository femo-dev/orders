using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace Orders.Api.Tests
{
    public class OrderEndpointsTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public OrderEndpointsTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_ValidRequest_ReturnsCreated()
        {
            var order = new
            {
                CustomerName = "Fabian",
                Items = new[]
                {
                new { ProductId = 1, Quantity = 2 }
            }
            };

            var response = await _client
                .PostAsJsonAsync("/api/orders", order);

            response.StatusCode.Should()
                .Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateOrder_InvalidProduct_Returns400()
        {
            var order = new
            {
                CustomerName = "Fabian",
                Items = new[]
                {
                new { ProductId = 999, Quantity = 2 }
            }
            };

            var response = await _client
                .PostAsJsonAsync("/api/orders", order);

            response.StatusCode.Should()
                .Be(HttpStatusCode.BadRequest);
        }
    }
}
