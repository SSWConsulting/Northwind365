using System.Dynamic;

using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Supplying;

public record SupplierId(Guid Value)
{
    public static SupplierId Create() => new(Guid.NewGuid());
}

public class Supplier : BaseEntity<SupplierId>
{
    private Supplier() { }

    public static Supplier Create(string companyName, string contactName, string contactTitle, Address address,
        string phone, string fax, string homePage)
    {
        var supplier = new Supplier()
        {
            Id = SupplierId.Create(),
            CompanyName = companyName,
            ContactName = contactName,
            ContactTitle = contactTitle,
            Address = address,
            Phone = phone,
            Fax = fax,
            HomePage = homePage
        };

        return supplier;
    }

    public string CompanyName { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Fax { get; private set; } = null!;
    public string HomePage { get; private set; } = null!;

    private readonly List<Product> _products = new();
    public IEnumerable<Product> Products => _products.AsReadOnly();
}