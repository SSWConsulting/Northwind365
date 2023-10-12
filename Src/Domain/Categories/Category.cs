using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Categories;

public readonly record struct CategoryId(int Value);

public class Category : BaseEntity<CategoryId>
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