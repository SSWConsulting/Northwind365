using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Shipping;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
{
    public void Configure(EntityTypeBuilder<Shipper> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("ShipperID")
            .HasConversion(e => e.Value, e => new ShipperId(e))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.Phone)
            .HasMaxLength(24);
    }
}