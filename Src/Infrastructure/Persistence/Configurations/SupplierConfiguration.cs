using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Supplying;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("SupplierID")
            .HasConversion(x => x.Value, x => new SupplierId(x))
            .ValueGeneratedOnAdd();

        builder.ComplexProperty(e => e.Address, AddressConfiguration.BuildAction);

        builder.Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.ContactName)
            .HasMaxLength(30);

        builder.Property(e => e.ContactTitle)
            .HasMaxLength(50);

        builder.Property(e => e.Fax)
            .HasMaxLength(24);

        builder.Property(e => e.HomePage)
            .HasColumnType("ntext");

        builder.Property(e => e.Phone)
            .HasMaxLength(24);

        // builder.HasMany(e => e.Products)
        //     .WithOne(p => p.Supplier)
        //     .HasForeignKey(fk => fk.Id)
        //     .IsRequired();
    }
}