using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record Country : ValueObject
{
    public Country(string countryName)
    {
        this.Name = Guard.Against.StringLength(countryName, 15);
    }

    // Needed for EF Core
    // ReSharper disable once UnusedMember.Local
    private Country() { }

    public bool IsAustralia => Name.Equals("Australia", StringComparison.OrdinalIgnoreCase);
    public string Name { get; } = null!;

    public void Deconstruct(out string name)
    {
        name = this.Name;
    }
}