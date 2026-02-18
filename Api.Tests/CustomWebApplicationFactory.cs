using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Infrastructure.Persistence;

namespace Api.Tests
{
    public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                // Seed data
                var product = Domain.Entities.Product.Create(
                    name: "Test Product",
                    price: 10,
                    stock: 100
                );

                db.Products.Add(product);

                db.SaveChanges();
            });
        }
    }
}
