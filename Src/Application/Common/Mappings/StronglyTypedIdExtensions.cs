using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Common.Mappings;

public static class StronglyTypedIdExtensions
{
    public static SupplierId? ToSupplierId(this int? integer) => integer == null ? null : new SupplierId(integer.Value);

    public static CategoryId? ToCategoryId(this int? integer) => integer == null ? null : new CategoryId(integer.Value);
}