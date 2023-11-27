using Northwind.Domain.Common.Base;
using Northwind.Domain.Products;

namespace Northwind.Domain.Categories;

public readonly record struct CategoryId(int Value);

public class Category(string categoryName, string description, byte[] picture) : BaseEntity<CategoryId>
{
    public string CategoryName { get; } = categoryName;
    public string Description { get; } = description;
    public byte[] Picture { get; } = picture;

    private readonly List<Product> _products = new();

    public IReadOnlyList<Product> Products => _products.AsReadOnly();
}