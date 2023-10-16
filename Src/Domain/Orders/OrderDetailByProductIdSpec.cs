using Ardalis.Specification;
using Northwind.Domain.Products;

namespace Northwind.Domain.Orders;

public sealed class OrderDetailByProductIdSpec : Specification<OrderDetail>
{
    public OrderDetailByProductIdSpec(ProductId productId)
    {
        Query.Where(od => od.ProductId == productId);
    }
}