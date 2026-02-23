using Orders.Domain.Entities;
using Orders.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración de la entidad OrderItem en Entity Framework Core.
    /// </summary>
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Tabla
            builder.ToTable("OrderItems");

            // Clave primaria
            builder.HasKey(oi => oi.Id);

            // Propiedades escalares
            builder.Property(oi => oi.ProductId)
                .IsRequired()
                .HasColumnName("ProductId");

            builder.Property(oi => oi.OrderId)
                .IsRequired()
                .HasColumnName("OrderId");

            builder.Property(oi => oi.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValue(DateTime.UtcNow);

            // Value Objects
            // Quantity
            builder.Property(oi => oi.Quantity)
                .HasConversion(
                    v => v.Value,
                    v => Quantity.Create(v))
                .IsRequired()
                .HasColumnName("Quantity");

            // Money (UnitPrice)
            builder.Property(oi => oi.UnitPrice)
                .HasConversion(
                    v => v.Amount,
                    v => Money.Create(v))
                .HasPrecision(18, 2)
                .IsRequired()
                .HasColumnName("UnitPrice");

            // Índices
            builder.HasIndex(oi => new { oi.OrderId, oi.ProductId })
                .HasDatabaseName("IX_OrderItems_OrderId_ProductId");
        }
    }
}
