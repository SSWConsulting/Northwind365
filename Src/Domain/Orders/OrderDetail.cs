﻿using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Orders;

public class OrderDetail : AuditableEntity
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}
