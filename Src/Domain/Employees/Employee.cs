using Ardalis.GuardClauses;

using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Employees;

public readonly record struct EmployeeId(int Value);

public class Employee : BaseEntity<EmployeeId>
{
    public string? UserId { get; private set; }
    public string LastName { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string TitleOfCourtesy { get; private set; } = null!;
    public DateTime? BirthDate { get; private set; }
    public DateTime? HireDate { get; private set; }
    public Address Address { get; private set; } = null!;
    public string HomePhone { get; private set; } = null!;
    public string Extension { get; private set; } = null!;
    public byte[] Photo { get; private set; } = null!;
    public string Notes { get; private set; } = null!;
    public EmployeeId? ReportsTo { get; private set; }
    public string PhotoPath { get; private set; } = null!;

    public Employee Manager { get; private set; } = null!;

    private readonly List<Territory> _territories = new();
    public IEnumerable<Territory> Territories => _territories.AsReadOnly();

    private readonly List<Employee> _directReports = new();
    public IEnumerable<Employee> DirectReports => _directReports.AsReadOnly();

    private readonly List<Order> _orders = new();
    public IEnumerable<Order> Orders => _orders.AsReadOnly();

    private Employee()
    {
    }

    public static Employee Create(DateTime birthDate, Address address, string extension, string firstName,
        string homePhone, DateTime hireDate, string lastName, string notes, string photoPath, byte[] photo,
        string title, string titleOfCourtesy, IEnumerable<Territory> territories)
    {
        var employee = new Employee
        {
            BirthDate = birthDate,
            Address = address,
            Extension = extension,
            FirstName = firstName,
            HomePhone = homePhone,
            HireDate = hireDate,
            LastName = lastName,
            Notes = notes,
            PhotoPath = photoPath,
            Photo = photo,
            Title = title,
            TitleOfCourtesy = titleOfCourtesy,
        };

        employee._territories.AddRange(territories);

        return employee;
    }

    public void UpdateUserId(string? userId)
    {
        UserId = Guard.Against.NullOrWhiteSpace(userId);
    }
}