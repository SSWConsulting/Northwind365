using System.Runtime.CompilerServices;

using Ardalis.GuardClauses;

using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Customers;

public record CustomerId(Guid Value);

public class Customer : BaseEntity<CustomerId>
{
    private Customer() { }

    public static Customer Create(string companyName, string contactName, string contactTitle, Address address,
        string phone, string fax)
    {
        var customer = new Customer()
        {
            Id = new CustomerId(Guid.NewGuid()), CompanyName = companyName, Phone = phone, Fax = fax,
        };

        customer.UpdateAddress(address);
        customer.UpdateContact(contactName, contactTitle);

        return customer;
    }

    public string CompanyName { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public string Phone { get; private set; } = null!;
    public string Fax { get; private set; } = null!;

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