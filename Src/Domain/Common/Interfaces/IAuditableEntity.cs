namespace Northwind.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; }
    public string? CreatedBy { get; }
    public DateTimeOffset? UpdatedAt { get; }
    public string? UpdatedBy { get; }

    public void SetCreated(DateTimeOffset now, string? by = null);

    public void SetUpdated(DateTimeOffset now, string? by = null);
}