using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Customers;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("CustomerID")
            .HasMaxLength(10)
            .HasConversion(x => x.Value,
                x => new CustomerId(x))
            .ValueGeneratedNever();

        builder.ComplexProperty(e => e.Address, AddressConfiguration.BuildAction);

        builder.Property(e => e.CompanyName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.ContactName).HasMaxLength(30);

        builder.Property(e => e.ContactTitle).HasMaxLength(50);

        builder.ComplexProperty(e => e.Fax, faxBuilder => faxBuilder.Property(m => m.Number).HasMaxLength(24));

        builder.ComplexProperty(e => e.Phone, phoneBuilder => phoneBuilder.Property(m => m.Number).HasMaxLength(24));

        builder.HasMany(e => e.Orders)
            .WithOne(p => p.Customer)
            .HasForeignKey(e => e.Id)
            .IsRequired();
    }
}