using Northwind.Domain.Common.Interfaces;

namespace Northwind.Domain.Common.Base;

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }
    public void SetCreated(DateTimeOffset now, string? by = null)
    {
        CreatedAt = now;
        CreatedBy = by;
    }

    public void SetUpdated(DateTimeOffset now, string? by = null)
    {
        UpdatedAt = now;
        UpdatedBy = by;
    }
}