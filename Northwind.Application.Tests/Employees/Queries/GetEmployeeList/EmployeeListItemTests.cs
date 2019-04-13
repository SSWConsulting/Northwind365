using Northwind.Application.Employees.Queries.GetAllEmployees;
using Northwind.Domain.Entities;
using Shouldly;
using Xunit;

namespace Northwind.Application.Tests.Employees.Queries.GetEmployeeList
{
    public class EmployeeListItemTests
    {
        [Fact]
        public void Create_GivenEmployee_ReturnsCorrectEmployeeListItemDto()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                TitleOfCourtesy = "Mr.",
                FirstName = "Jason",
                LastName = "Taylor",
                Title = "Solution Architect",
                City = "Brisbane",
                Region = "",
                Country = "Australia"
            };

            var employeeListItem = EmployeeListItem.Create(employee);

            employeeListItem.Id.ShouldBe(1);
            employeeListItem.Name.ShouldBe("Mr. Jason Taylor");
            employeeListItem.Location.ShouldBe("Brisbane, Australia");
            employeeListItem.Position.ShouldBe("Solution Architect");
        }
    }
}
