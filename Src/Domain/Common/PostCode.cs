using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record PostCode : ValueObject
{
    public PostCode(string number)
    {
        this.Number = Guard.Against.StringLength(number, 30);
    }

    public bool IsQueenslandPostCode => Number.StartsWith('4');
    public string Number { get; }

    public void Deconstruct(out string number)
    {
        number = this.Number;
    }
}