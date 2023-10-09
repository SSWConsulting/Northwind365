using Northwind.Domain.Categories;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Orders;
using Northwind.Domain.Supplying;

namespace Northwind.Domain.Products;

public class Product : AuditableEntity
{
    public Product()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public SupplierId? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; private set; }
}