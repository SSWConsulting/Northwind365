namespace Northwind.Domain.Employees;

// NOTE: No base entity needed as this is a pure joining table
public class EmployeeTerritory
{
    public EmployeeId EmployeeId { get; private set; } = null!;
    public TerritoryId TerritoryId { get; private set; } = null!;

    public Employee? Employee { get; private set; }
    public Territory? Territory { get; private set; }

    private EmployeeTerritory() { }

    // public static EmployeeTerritory Create(EmployeeId employeeId, TerritoryId territoryId)
    // {
    //     var employeeTerritory = new EmployeeTerritory { EmployeeId = employeeId, TerritoryId = territoryId };
    //     return employeeTerritory;
    // }
}