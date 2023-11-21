using Ardalis.Specification;

namespace Northwind.Domain.Customers;

public sealed class CustomerByIdSpec : SingleResultSpecification<Customer>
{
    public CustomerByIdSpec(CustomerId customerId)
    {
        Query
            .Include(c => c.Orders)
            .Where(c => c.Id == customerId);
    }
}