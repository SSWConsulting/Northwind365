using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Northwind.Domain.Customers;
using Northwind.Domain.Orders;

namespace Northwind.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.OrderId).HasColumnName("OrderID");

        builder.Property(e => e.CustomerId)
            .HasColumnName("CustomerID")
            .HasMaxLength(10)
            .HasConversion(x => x.Value,
                x => new CustomerId(x));

        builder.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

        builder.Property(e => e.Freight)
            .HasColumnType("money")
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.OrderDate).HasColumnType("datetime");

        builder.Property(e => e.RequiredDate).HasColumnType("datetime");

        builder.Property(e => e.ShipName).HasMaxLength(40);

        builder.OwnsOne(e => e.ShipAddress, AddressConfiguration.BuildAction);

        builder.Property(e => e.ShippedDate).HasColumnType("datetime");

        builder.HasOne(d => d.Shipper)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.ShipVia)
            .HasConstraintName("FK_Orders_Shippers");
    }
}