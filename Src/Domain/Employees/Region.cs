using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Employees;

public readonly record struct RegionId(int Value);

public class Region : BaseEntity<RegionId>
{
    public string RegionDescription { get; private set; } = null!;

    private readonly List<Territory> _territories = new();
    public IEnumerable<Territory> Territories => _territories.AsReadOnly();

    private Region() { }

    public static Region Create(string description)
    {
        var region = new Region { RegionDescription = description };
        return region;
    }
}