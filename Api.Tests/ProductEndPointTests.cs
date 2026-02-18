using System.Net;
using FluentAssertions;

namespace Api.Tests
{
    public class ProductEndpointsTests
    : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductEndpointsTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ReturnsOk()
        {
            var response = await _client
                .GetAsync("/api/products?pageNumber=1&pageSize=10");

            response.StatusCode.Should()
                .Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetProduct_NotExists_Returns404()
        {
            var response = await _client
                .GetAsync("/api/products/999");

            response.StatusCode.Should()
                .Be(HttpStatusCode.NotFound);
        }
    }
}

