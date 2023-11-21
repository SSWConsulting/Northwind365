using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Common;

namespace Northwind.Infrastructure.Persistence.Configurations;

internal static class AddressConfiguration
{
    internal static void BuildAction(ComplexPropertyBuilder<Address> addressBuilder)
    {
        addressBuilder.Property(m => m.Line1).HasMaxLength(60);
        addressBuilder.Property(m => m.City).HasMaxLength(50);
        addressBuilder.Property(m => m.Region).HasMaxLength(100);

        addressBuilder.ComplexProperty(m => m.PostalCode, builder => builder.Property(m => m.Number).HasMaxLength(10));
        addressBuilder.ComplexProperty(m => m.Country, builder => builder.Property(m => m.Name).HasMaxLength(100));
    }
}