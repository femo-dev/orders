using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de la entidad Product en Entity Framework Core.
    /// Define las propiedades, restricciones y mapeos a la base de datos.
    /// </summary>
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Tabla
            builder.ToTable("Products");

            // Clave primaria
            builder.HasKey(p => p.Id);

            // Propiedades escalares
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("Name");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValue(DateTime.UtcNow);

            // Propiedades de Value Objects
            // Money (Price)
            builder.Property(p => p.Price)
                .HasConversion(
                    v => v.Amount,
                    v => Money.Create(v))
                .HasPrecision(18, 2)
                .IsRequired()
                .HasColumnName("Price");

            // Stock
            builder.Property(p => p.Stock)
                .HasConversion(
                    v => v.Quantity,
                    v => Stock.Create(v))
                .IsRequired()
                .HasColumnName("Stock");

            // Índices
            builder.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");
        }
    }
}
