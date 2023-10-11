using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Products;
using Northwind.Domain.Shipping;

namespace Northwind.Domain.Orders;

public readonly record struct OrderId(int Value);

public class Order : BaseEntity<OrderId>
{
    public CustomerId CustomerId { get; private set; }
    public EmployeeId? EmployeeId { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public DateTime? RequiredDate { get; private set; }
    public DateTime? ShippedDate { get; private set; }
    public ShipperId? ShipVia { get; private set; }
    public decimal? Freight { get; private set; }
    public string ShipName { get; private set; } = null!;

    public Address ShipAddress { get; private set; } = null!;

    public Customer Customer { get; private set; } = null!;
    public Employee? Employee { get; private set; }
    public Shipper? Shipper { get; private set; }


    private List<OrderDetail> _orderDetails = new();
    public IEnumerable<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    private Order() { }

    public static Order Create(CustomerId customerId, EmployeeId? employeeId, DateTime orderDate, DateTime requiredDate,
        DateTime shippedDate, ShipperId shipper, decimal freight, string shipName, Address address)
    {
        var order = new Order
        {
            CustomerId = customerId,
            EmployeeId = employeeId,
            OrderDate = orderDate,
            RequiredDate = requiredDate,
            ShippedDate = shippedDate,
            ShipVia = shipper,
            Freight = freight,
            ShipName = shipName,
            ShipAddress = address
        };

        return order;
    }

    public void AddOrderDetails(ProductId productId, decimal unitPrice, short quantity, float discount)
    {
        var existing = _orderDetails.SingleOrDefault(x => x.ProductId == productId);
        if (existing != null)
        {
            existing.AddQuantity(quantity);
        }
        else
        {
            var orderDetail = OrderDetail.Create(Id, productId, unitPrice, quantity, discount);
            _orderDetails.Add(orderDetail);
        }
    }
}