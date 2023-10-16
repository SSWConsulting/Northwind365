using Ardalis.Specification;

namespace Northwind.Domain.Customers;

public sealed class CustomerByIdSpec : SingleResultSpecification<Customer>
{
    public CustomerByIdSpec(CustomerId customerId)
    {
        Query.Where(c => c.Id == customerId);
    }
}