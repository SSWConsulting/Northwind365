using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Customers;

public record CustomerCreatedEvent(CustomerId CustomerId) : DomainEvent;