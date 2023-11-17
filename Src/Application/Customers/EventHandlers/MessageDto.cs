namespace Northwind.Application.Customers.EventHandlers;

public record MessageDto(string From, string To, string Subject, string Body);