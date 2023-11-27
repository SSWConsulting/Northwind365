using Ardalis.GuardClauses;
using Northwind.Domain.Common.Base;

namespace Northwind.Domain.Customers;

public record Phone : ValueObject
{
    public Phone(string phoneNumber)
    {
        this.Number = Guard.Against.StringLength(phoneNumber, 24) ;
    }

    // Needed for EF Core
    // ReSharper disable once UnusedMember.Local
    private Phone() { }

    public bool IsQueenslandLandLine => Number.StartsWith("07");
    public string Number { get; } = null!;

    public void Deconstruct(out string number)
    {
        number = this.Number;
    }
}