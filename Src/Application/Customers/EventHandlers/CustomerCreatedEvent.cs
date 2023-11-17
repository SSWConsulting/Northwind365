using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.EventHandlers;

// ReSharper disable once UnusedType.Global
public class CustomerCreatedHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly INotificationService _notification;

    public CustomerCreatedHandler(INotificationService notification)
    {
        _notification = notification;
    }

    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Publish notification to external service so welcome email can be sent
        await _notification.SendAsync(new MessageDto("From", "To", "Subject - Welcome to Northwind365", "Body"));
    }
}