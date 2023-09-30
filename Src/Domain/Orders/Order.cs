using Northwind.Domain.Common;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Shipping;

namespace Northwind.Domain.Orders;

public class Order : AuditableEntity
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public int OrderId { get; set; }
    public CustomerId CustomerId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int? ShipVia { get; set; }
    public decimal? Freight { get; set; }
    public string ShipName { get; set; }

    public Address ShipAddress { get; set; } = null!;

    public Customer Customer { get; set; }
    public Employee Employee { get; set; }
    public Shipper Shipper { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; private set; }

    public void AddOrderDetails(OrderDetail detail)
    {
        var existing = OrderDetails.SingleOrDefault(x => x.ProductId == detail.ProductId);
        if (existing != null)
        {
            existing.Quantity += detail.Quantity;
        }
        else
        {
            OrderDetails.Add(detail);
        }
    }

    public void AddOrderDetails(IEnumerable<OrderDetail> details)
    {
        foreach (var detail in details)
            this.AddOrderDetails(detail);
    }
}