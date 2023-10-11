using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Employees;

public record TerritoryId(string Value);

public class Territory : BaseEntity<TerritoryId>
{
    public string TerritoryDescription { get; private set; } = null!;
    public RegionId RegionId { get; private set; } = null!;

    public Region? Region { get; private set; }

    private readonly List<Employee> _employees = new();
    public IEnumerable<Employee> Employees => _employees.AsReadOnly();

    private Territory() { }

    public static Territory Create(TerritoryId territoryId, RegionId regionId, string description)
    {
        var territory = new Territory { Id = territoryId, RegionId = regionId, TerritoryDescription = description };

        return territory;
    }
}