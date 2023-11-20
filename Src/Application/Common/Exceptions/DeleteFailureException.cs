using System;

namespace Northwind.Application.Common.Exceptions;

public class DeleteFailureException(string name, object key, string message) : Exception($"Deletion of entity \"{name}\" ({key}) failed. {message}");
