namespace Northwind.Application.Products.Queries.GetProductsList;

public class ProductsListVm
{
    public required IList<ProductDto> Products { get; init; }

    public bool CreateEnabled { get; set; }
}