using System;

namespace Northwind.Domain.Common;

public class AuditableEntity
{
    public string CreatedBy { get; set; } = null!;

    public DateTime Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }
}
