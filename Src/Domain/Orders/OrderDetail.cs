using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Orders;

// NOTE: Note inheriting from BaseEntity as we have a composite PK
public class OrderDetail : AuditableEntity
{
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public short Quantity { get; private set; }
    public float Discount { get; private set; }

    public Order? Order { get; private set; }
    public Product Product { get; private set; } = null!;

    private OrderDetail() { }

    internal static OrderDetail Create(OrderId orderId, ProductId productId, decimal unitPrice, short quantity, float discount)
    {
        var orderDetail = new OrderDetail
        {
            ProductId = productId,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Discount = discount
        };

        return orderDetail;
    }

    internal void AddQuantity(short quantity)
    {
        Quantity = quantity;
    }
}