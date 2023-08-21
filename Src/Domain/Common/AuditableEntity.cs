using System;

namespace Northwind.Domain.Common;

public class AuditableEntity
{
    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTimeOffset? LastModified { get; set; }
}
