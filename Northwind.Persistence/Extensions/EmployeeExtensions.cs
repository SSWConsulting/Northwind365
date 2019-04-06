using Northwind.Domain.Entities;

namespace Northwind.Persistence.Extensions
{
    internal static class EmployeeExtensions
    {
        public static Employee AddTerritories(this Employee employee, params EmployeeTerritory[] territories)
        {
            foreach (var territory in territories)
            {
                employee.EmployeeTerritories.Add(territory);
            }

            return employee;
        }
    }
}
