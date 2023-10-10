using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Common.Mappings;

public static class StronglyTypedIdExtensions
{
    public static SupplierId? ToSupplierId(this int? integer) => integer == null ? null : new SupplierId(integer.Value);

    //public static CustomerId? ToCustomerId(this string? str) => str == null ? null : new CustomerId(str.Value);

    // public static ProductId? ToProductId(this int? integer) => integer == null ? null : new ProductId(integer.Value);
    //
    // public static ProductId ToProductId(this int integer) => new ProductId(integer);

    public static CategoryId? ToCategoryId(this int? integer) => integer == null ? null : new CategoryId(integer.Value);
}