using Northwind.Domain.Common;
using Northwind.Domain.Products;

namespace Northwind.Domain.Supplying;

public class Supplier
{
    public Supplier()
    {
        Products = new HashSet<Product>();
    }

    public int SupplierId { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public Address Address { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string HomePage { get; set; }

    public ICollection<Product> Products { get; private set; }
}