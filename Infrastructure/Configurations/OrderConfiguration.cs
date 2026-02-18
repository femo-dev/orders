using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    /// <summary>  
    /// Configuración de la entidad Order en Entity Framework Core.  
    /// </summary>  
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Tabladotnet restore  

            builder.ToTable("Orders"); // Ensure the correct namespace is referenced for this method  

            // Clave primaria  
            builder.HasKey(o => o.Id);

            // Propiedades escalares  
            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("CustomerName");

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt")
                .HasDefaultValue(DateTime.UtcNow);

            // Relaciones  
            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices  
            builder.HasIndex(o => o.CustomerName)
                .HasDatabaseName("IX_Orders_CustomerName");
        }
    }
}
