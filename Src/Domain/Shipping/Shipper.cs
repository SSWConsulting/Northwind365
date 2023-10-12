using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Shipping;

public readonly record struct ShipperId(int Value);

public class Shipper : BaseEntity<ShipperId>
{
    public string CompanyName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;

    private List<Order> _orders = new();

    public IEnumerable<Order> Orders => _orders.AsReadOnly();

    private Shipper() { }

    public static Shipper Create(string companyName, string phoneNumber)
    {
        var shipper = new Shipper { CompanyName = companyName, Phone = phoneNumber };
        return shipper;
    }
}