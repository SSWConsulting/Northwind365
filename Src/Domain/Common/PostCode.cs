using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Common;

public record PostCode : ValueObject
{
    public PostCode(string postCode)
    {
        this.Number = Guard.Against.StringLength(postCode, 30);
    }

    // Needed for EF Core
    // ReSharper disable once UnusedMember.Local
    private PostCode() { }

    public bool IsQueenslandPostCode => Number.StartsWith('4');
    public string Number { get; } = null!;

    public void Deconstruct(out string number)
    {
        number = this.Number;
    }
}