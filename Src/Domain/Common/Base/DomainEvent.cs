using MediatR;

namespace Northwind.Domain.Common.Base;

public abstract record DomainEvent : INotification;