using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Northwind.Application.Common.Exceptions;

public class ValidationException() : Exception("One or more validation failures have occurred.")
{
    public ValidationException(List<ValidationFailure> failures)
        : this()
    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();
}
