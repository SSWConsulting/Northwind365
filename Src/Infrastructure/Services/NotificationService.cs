using Northwind.Application.Common.Interfaces;
using Northwind.Application.Customers.EventHandlers;

namespace Northwind.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public Task SendAsync(MessageDto message)
    {
        return Task.CompletedTask;
    }
}
