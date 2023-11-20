using Ardalis.GuardClauses;
using System.Runtime.CompilerServices;

// Using the same namespace will make sure your code picks up your
// extensions no matter where they are in your codebase.
// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses;

public static class GuardClauseExt
{
    public static string StringLength(this IGuardClause guardClause,
        string input,
        int maxLength,
        [CallerArgumentExpression("input")] string? parameterName = null)
    {
        if (input?.Length > maxLength)
            throw new ArgumentException($"Cannot exceed string length of {maxLength}", parameterName);

        return input!;
    }

    public static short Negative(this IGuardClause guardClause,
        short input,
        [CallerArgumentExpression("input")] string? parameterName = null)
    {
        if (input < 0)
            throw new ArgumentException($"Value cannot be negative", parameterName);

        return input;
    }
}