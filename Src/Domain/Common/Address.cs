using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record Address : ValueObject
{
    // NOTE: Need to use a static method as EF does not support complex type injection via constructors
    // https://github.com/dotnet/efcore/issues/31621
    public static Address Create(string line1, string city, string region, PostCode postalCode, Country country)
    {
        var address = new Address
        {
            Line1 = line1,
            City = city,
            Region = region,
            PostalCode = postalCode,
            Country = country
        };

        return address;
    }

    private Address() { }

    public required string Line1 { get; init; }
    public required string City { get; init; }
    public required string Region { get; init; }
    public required PostCode PostalCode { get; init; }
    public required Country Country { get; init; }
}