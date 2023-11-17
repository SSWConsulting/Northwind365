using Northwind.Application.Customers.EventHandlers;
using System.Threading.Tasks;

namespace Northwind.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendAsync(MessageDto message);
}
