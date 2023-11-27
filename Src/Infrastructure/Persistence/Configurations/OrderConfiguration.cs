using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Orders;
using Northwind.Domain.Shipping;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("OrderID")
            .HasConversion(e => e.Value, e => new OrderId(e))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CustomerId)
            .HasColumnName("CustomerID")
            .HasMaxLength(10)
            .HasConversion(e => e.Value, e => new CustomerId(e));

         builder.Property(e => e.EmployeeId)
             .HasColumnName("EmployeeID")
             // TODO: Test this with null FK values
             .HasConversion(x => x!.Value.Value, x => new EmployeeId(x));

        builder.Property(e => e.ShipVia)
            .HasConversion(e => e!.Value.Value, e => new ShipperId(e));

        builder.Property(e => e.Freight)
            .HasColumnType("money")
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.OrderDate).HasColumnType("datetime");

        builder.Property(e => e.RequiredDate).HasColumnType("datetime");

        builder.Property(e => e.ShipName).HasMaxLength(40);

        builder.ComplexProperty(e => e.ShipAddress, AddressConfiguration.BuildAction);

        builder.Property(e => e.ShippedDate).HasColumnType("datetime");

        builder.HasOne(e => e.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(e => e.CustomerId);

        builder.HasOne(e => e.Employee)
            .WithMany(p => p.Orders)
            .HasForeignKey(e => e.EmployeeId);

        builder.HasOne(d => d.Shipper)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.ShipVia)
            .HasConstraintName("FK_Orders_Shippers");
    }
}