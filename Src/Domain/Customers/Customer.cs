using System.Runtime.CompilerServices;

using Ardalis.GuardClauses;

using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Customers;

public record CustomerId(Guid Value);

public class Customer : BaseEntity<CustomerId>
{
    public Customer(string companyName, string contactName, string contactTitle, Address address, string phone, string fax)
    {
        Id = new CustomerId(Guid.NewGuid());
        CompanyName = companyName;
        Phone = phone;
        Fax = fax;
        UpdateAddress(address);
        UpdateContact(contactName, contactTitle);
    }

    public string CompanyName { get; private set; }
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public string Phone { get; private set; }
    public string Fax { get; private set; }

    private List<Order> _orders = new();

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public void UpdateAddress(Address address)
    {
        Address = address;
    }

    public void UpdateContact(string contactName, string contactTitle)
    {
        ContactName = Guard.Against.StringLength(contactName, 30);
        ContactTitle = Guard.Against.StringLength(contactTitle, 50);
    }

    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }

    public void UpdateFax(string fax)
    {
        Fax = fax;
    }

    public void UpdateCompanyName(string companyName)
    {
        CompanyName = companyName;
    }
}

public static class StringLengthGuard
{
    public static string StringLength(this IGuardClause guardClause,
        string input,
        int maxLength,
        [CallerArgumentExpression("input")] string? parameterName = null)
    {
        if (input?.Length > maxLength)
            throw new ArgumentException($"Cannot exceed string length of {maxLength}", parameterName);

        return input!;
    }
}