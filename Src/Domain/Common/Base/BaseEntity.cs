namespace Northwind.Domain.Common.Base;

public abstract class BaseEntity<TId> : AuditableEntity
{
    public TId Id { get; set; } = default!;
}