using Ardalis.GuardClauses;
using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Common.Exceptions;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Customers;

public readonly record struct CustomerId(string Value);

public class Customer : AggregateRoot<CustomerId>
{
    private Customer() { }

    public static Customer Create(CustomerId customerId, string companyName, string contactName, string contactTitle,
        Address address, Phone phone, Phone fax)
    {
        var customer = new Customer()
        {
            Id = customerId, CompanyName = companyName, Phone = phone, Fax = fax,
        };

        customer.UpdateAddress(address);
        customer.UpdateContact(contactName, contactTitle);

        customer.AddDomainEvent(new CustomerCreatedEvent(customer.Id));

        return customer;
    }

    public string CompanyName { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public Phone Phone { get; private set; } = null!;
    public Phone Fax { get; private set; } = null!;

    private readonly List<Order> _orders = new();

    public IEnumerable<Order> Orders => _orders.AsReadOnly();

    public void UpdateAddress(Address address)
    {
        Address = address;
    }

    public void UpdateContact(string contactName, string contactTitle)
    {
        ContactName = Guard.Against.StringLength(contactName, 30);
        ContactTitle = Guard.Against.StringLength(contactTitle, 50);
    }

    public void UpdatePhone(Phone phone)
    {
        if (Address.Country.IsAustralia && Address.PostalCode.IsQueenslandPostCode)
        {
            if (!phone.IsQueenslandLandLine)
            {
                throw new DomainException("Queensland customers must have a Queensland phone number.");
            }
        }

        Phone = phone;
    }

    public void UpdateFax(Phone fax)
    {
        if (Address.Country.IsAustralia && Address.PostalCode.IsQueenslandPostCode)
        {
            if (!fax.IsQueenslandLandLine)
            {
                throw new DomainException("Queensland customers must have a Queensland fax number.");
            }
        }

        Fax = fax;
    }

    public void UpdateCompanyName(string companyName)
    {
        CompanyName = Guard.Against.NullOrWhiteSpace(companyName);
    }

    public bool CanDelete()
    {
        return !_orders.Any();
    }
}