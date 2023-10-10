using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Common.Mappings;

public static class StronglyTypedIdExtensions
{
    public static SupplierId? ToSupplierId(this Guid? guid) => guid == null ? null : new SupplierId(guid.Value);

    public static CustomerId? ToCustomerId(this Guid? guid) => guid == null ? null : new CustomerId(guid.Value);

    public static ProductId? ToProductId(this int? integer) => integer == null ? null : new ProductId(integer.Value);

    public static ProductId ToProductId(this int integer) => new ProductId(integer);

    public static CategoryId? ToCategoryId(this int? integer) => integer == null ? null : new CategoryId(integer.Value);
}