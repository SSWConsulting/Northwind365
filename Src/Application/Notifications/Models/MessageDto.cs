namespace Northwind.Application.Notifications.Models;

public record MessageDto(string From, string To, string Subject, string Body);