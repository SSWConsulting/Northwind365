using Northwind.Application.Common.Interfaces;
using Northwind.Application.Notifications.Models;

namespace Northwind.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public Task SendAsync(MessageDto message)
    {
        return Task.CompletedTask;
    }
}
