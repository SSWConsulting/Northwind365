﻿namespace Northwind.Domain.Common.Interfaces;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; }
    public string? CreatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/76)
    public DateTimeOffset? UpdatedAt { get; }
    public string? UpdatedBy { get; } // TODO: String as userId? (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/76)

    public void SetCreated(DateTimeOffset now, string? by = null);

    public void SetUpdated(DateTimeOffset now, string? by = null);
}