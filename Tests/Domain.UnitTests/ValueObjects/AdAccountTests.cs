using FluentAssertions;
using Northwind.Domain.Common;
using Northwind.Domain.Exceptions;

using Xunit;

namespace Northwind.Domain.UnitTests.ValueObjects;

public class AdAccountTests
{
    [Fact]
    public void ShouldHaveCorrectDomainAndName()
    {
        var account = AdAccount.For("SSW\\Daniel");

        account.Domain.Should().Be("SSW");
        account.Name.Should().Be("Daniel");
    }

    [Fact]
    public void ToStringReturnsCorrectFormat()
    {
        const string value = "SSW\\Daniel";

        var account = AdAccount.For(value);

        account.ToString().Should().Be(value);
    }

    [Fact]
    public void ImplicitConversionToStringResultsInCorrectString()
    {
        const string value = "SSW\\Daniel";

        var account = AdAccount.For(value);

        string result = account;

        result.Should().Be(value);
    }

    [Fact]
    public void ExplicitConversionFromStringSetsDomainAndName()
    {
        var account = (AdAccount) "SSW\\Daniel";

        account.Domain.Should().Be("SSW");
        account.Name.Should().Be("Daniel");
    }

    [Fact]
    public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
    {
        FluentActions
            .Invoking(() => AdAccount.For("SSWDaniel"))
            .Should().Throw<AdAccountInvalidException>();
    }
}