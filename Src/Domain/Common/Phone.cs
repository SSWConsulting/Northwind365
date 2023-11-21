using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record Phone : ValueObject
{
    public Phone(string number)
    {
        this.Number = Guard.Against.StringLength(number, 24) ;
    }

    public bool IsQueenslandLandLine => Number.StartsWith("07");
    public string Number { get; }

    public void Deconstruct(out string number)
    {
        number = this.Number;
    }
}