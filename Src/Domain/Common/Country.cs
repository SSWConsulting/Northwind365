using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record Country : ValueObject
{
    public Country(string name)
    {
        this.Name = Guard.Against.StringLength(name, 15);
    }

    public bool IsAustralia => Name.Equals("Australia", StringComparison.OrdinalIgnoreCase);
    public string Name { get; }

    public void Deconstruct(out string name)
    {
        name = this.Name;
    }
}