using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Customers;

public class Customer : BaseEntity<string>
{
    public Customer(string companyName, string contactName, string contactTitle, string address, string city,
        string region, string postalCode, string country, string phone, string fax)
    {
        CompanyName = companyName;
        ContactName = contactName;
        ContactTitle = contactTitle;
        Address = address;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Country = country;
        Phone = phone;
        Fax = fax;
    }

    public string CompanyName { get; private set; }
    public string ContactName { get; private set; }
    public string ContactTitle { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string Region { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }
    public string Phone { get; private set; }
    public string Fax { get; private set; }

    private List<Order> _orders = new();

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public void UpdateAddress(string address, string postalCode, string city, string region, string country)
    {
        Address = address;
        PostalCode = postalCode;
        City = city;
        Region = region;
        Country = country;
    }

    public void UpdateContact(string contactName, string contactTitle)
    {
        ContactName = contactName;
        ContactTitle = contactTitle;
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