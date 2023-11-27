using Ardalis.GuardClauses;
using Northwind.Domain.Categories;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;
using Northwind.Domain.Supplying;

namespace Northwind.Domain.Products;

public readonly record struct ProductId(int Value);

public class Product : BaseEntity<ProductId>
{
    public string ProductName { get; private set; } = null!;
    public SupplierId? SupplierId { get; private set; }
    public CategoryId? CategoryId { get; private set; }
    public string? QuantityPerUnit { get; private set; }
    public decimal? UnitPrice { get; private set; }
    public short? UnitsInStock { get; private set; }
    public short? UnitsOnOrder { get; private set; }
    public short? ReorderLevel { get; private set; }
    public bool Discontinued { get; private set; }

    public Category? Category { get; private set; } = null!;
    public Supplier? Supplier { get; private set; } = null!;

    private List<OrderDetail> _orderDetails = new();

    public IEnumerable<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    private Product() { }

    public static Product Create(string productName, CategoryId? categoryId, SupplierId? supplierId,
        decimal? unitPrice, bool discontinued)
    {
        var product = new Product { UnitPrice = unitPrice };

        product.UpdateProduct(productName, categoryId, supplierId, discontinued);

        return product;
    }

    public void UpdateProduct(string productName, CategoryId? categoryId, SupplierId? supplierId,
        bool discontinued)
    {
        ProductName = Guard.Against.NullOrWhiteSpace(productName);
        CategoryId = categoryId;
        SupplierId = supplierId;
        Discontinued = discontinued;
    }

    public void UpdateQuantityPerUnit(string description)
    {
        QuantityPerUnit = description;
    }

    public void UpdateUnitsInStock(short s)
    {
        UnitsInStock = Guard.Against.Negative(s);
    }

    public void UpdateUnitsOnOrder(short s)
    {
        UnitsOnOrder = Guard.Against.Negative(s);
    }

    public void UpdateReorderLevel(short s)
    {
        ReorderLevel = Guard.Against.Negative(s);
    }
}