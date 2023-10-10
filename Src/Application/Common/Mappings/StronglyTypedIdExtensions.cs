using Northwind.Domain.Customers;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Common.Mappings;

public static class StronglyTypedIdExtensions
{
    public static SupplierId? ToSupplierId(this Guid? guid) => guid == null ? null : new SupplierId(guid.Value);

    public static CustomerId? ToCustomerId(this Guid? guid) => guid == null ? null : new CustomerId(guid.Value);
}