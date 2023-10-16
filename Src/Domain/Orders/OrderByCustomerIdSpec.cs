using Ardalis.Specification;
using Northwind.Domain.Customers;

namespace Northwind.Domain.Orders;

public sealed class OrderByCustomerIdSpec : Specification<Order>
{
    public OrderByCustomerIdSpec(CustomerId customerId)
    {
        Query.Where(c => c.CustomerId == customerId);
    }
}