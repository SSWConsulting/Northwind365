using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Northwind.Domain.Supplying;

namespace Northwind.Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.Property(e => e.SupplierId).HasColumnName("SupplierID");

        builder.OwnsOne(e => e.Address, AddressConfiguration.BuildAction);

        builder.Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.ContactName).HasMaxLength(30);

        builder.Property(e => e.ContactTitle).HasMaxLength(30);

        builder.Property(e => e.Fax).HasMaxLength(24);

        builder.Property(e => e.HomePage).HasColumnType("ntext");

        builder.Property(e => e.Phone).HasMaxLength(24);
    }
}