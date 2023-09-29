using Northwind.Domain.Common.Interfaces;

namespace Northwind.Domain.Common.Base;

// public class AuditableEntity
// {
//     public string CreatedBy { get; set; } = null!;
//
//     public DateTimeOffset Created { get; set; }
//
//     public string? LastModifiedBy { get; set; }
//
//     public DateTimeOffset? LastModified { get; set; }
// }

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}