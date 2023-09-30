using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Categories;

public class Category : BaseEntity<int>
{
    public Category(string categoryName, string description, byte[] picture)
    {
        CategoryName = categoryName;
        Description = description;
        Picture = picture;
    }

    public string CategoryName { get; }
    public string Description { get; }
    public byte[] Picture { get; }

    private readonly List<Product> _products = new();

    public IReadOnlyList<Product> Products => _products.AsReadOnly();
}