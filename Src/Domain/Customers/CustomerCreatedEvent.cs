using MediatR;

namespace Northwind.Domain.Customers;

public record CustomerCreatedEvent(CustomerId CustomerId) : INotification;