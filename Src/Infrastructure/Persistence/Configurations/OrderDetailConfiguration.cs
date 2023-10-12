using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId });

        builder.ToTable("Order Details");

        builder.Property(e => e.OrderId)
            .HasColumnName("OrderID")
            .HasConversion(e => e.Value, e => new OrderId(e))
            .ValueGeneratedNever();

        builder.Property(e => e.ProductId)
            .HasColumnName("ProductID")
            .HasConversion(e => e.Value, e => new ProductId(e))
            .ValueGeneratedNever();

        builder.Property(e => e.Quantity)
            .HasDefaultValueSql("((1))");

        builder.Property(e => e.UnitPrice)
            .HasColumnType("money");

        builder.HasOne(d => d.Order)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_Details_Orders");

        builder.HasOne(d => d.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Order_Details_Products");
    }
}