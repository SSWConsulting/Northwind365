using System;
using System.Linq;
using System.Linq.Expressions;
using Northwind.Domain.Entities;

namespace Northwind.Application.Employees.Queries.GetAllEmployees
{
    public class EmployeeListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public static Expression<Func<Employee, EmployeeListItem>> Projection
        {
            get
            {
                return employee => new EmployeeListItem
                {
                    Id = employee.EmployeeId,
                    Name = BuildName(employee),
                    Position = employee.Title,
                    Location = BuildLocation(employee)
                };
            }
        }

        public static EmployeeListItem Create(Employee employee)
        {
            return Projection.Compile().Invoke(employee);
        }

        private static string BuildName(Employee employee)
        {
            return
                $"{employee.TitleOfCourtesy} {employee.FirstName} {employee.LastName}";
        }

        private static string BuildLocation(Employee employee)
        {
            var elements = new[]
                {
                    employee.City,
                    employee.Region,
                    employee.Country
                }
                .Where(l => !string.IsNullOrWhiteSpace(l));

            return string.Join(", ", elements);
        }
    }
}
