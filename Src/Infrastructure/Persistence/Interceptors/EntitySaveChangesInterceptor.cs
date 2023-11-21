using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Common.Interfaces;

namespace Northwind.Infrastructure.Persistence.Interceptors;

public class EntitySaveChangesInterceptor
    (ICurrentUserService currentUserService, IDateTime dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context is null)
            return;

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
            if (entry.State is EntityState.Added)
            {
                entry.Entity.SetCreated(dateTime.Now, currentUserService.GetUserId());
            }
            else if (entry.State is EntityState.Added or EntityState.Modified ||
                     entry.HasChangedOwnedEntities())
            {
                entry.Entity.SetUpdated(dateTime.Now, currentUserService.GetUserId());
            }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}