using Api.Extensions;
using Api.Middleware;
using Infrastructure.Persistence;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Servicios de Infrastructure
builder.Services.AddInfrastructureServices("");

// Servicios de Application
builder.Services.AddApplicationServices();

// Logging
builder.Services.AddLogging();

var app = builder.Build();

// Middleware personalizado para manejo de errores
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Productos Órdenes API v1");
        c.RoutePrefix = "swagger"; // Para acceder a Swagger en la raíz
    });
}

// CORS (opcional, si necesitas acceso desde otro dominio)
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

// Pipeline HTTP
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed de datos iniciales
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    await SeedData.InitializeSeedAsync(dbContext);
}

app.Run();