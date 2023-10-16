using Ardalis.Specification;

namespace Northwind.Domain.Products;

public sealed class ProductByIdSpec : SingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId productId)
    {
        Query.Where(c => c.Id == productId);
    }
}