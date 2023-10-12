using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Common;

namespace Northwind.Infrastructure.Persistence.Configurations;

internal static class AddressConfiguration
{
    internal static void BuildAction<T>(OwnedNavigationBuilder<T, Address> priceBuilder) where T : class
    {
        priceBuilder.Property(m => m.Line1).HasMaxLength(60);
        priceBuilder.Property(m => m.City).HasMaxLength(50);
        priceBuilder.Property(m => m.PostalCode).HasMaxLength(10);
        priceBuilder.Property(m => m.Region).HasMaxLength(100);
        priceBuilder.Property(m => m.Country).HasMaxLength(100);
    }
}