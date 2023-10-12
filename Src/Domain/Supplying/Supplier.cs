using System.Dynamic;

using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Supplying;

public readonly record struct SupplierId(int Value);

public class Supplier : BaseEntity<SupplierId>
{
    public string CompanyName { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public string ContactTitle { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Fax { get; private set; } = null!;
    public string HomePage { get; private set; } = null!;

    private readonly List<Product> _products = new();
    public IEnumerable<Product> Products => _products.AsReadOnly();

    private Supplier() { }

    public static Supplier Create(string companyName, string contactName, string contactTitle, Address address,
        string phone, string fax, string homePage)
    {
        var supplier = new Supplier()
        {
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
}