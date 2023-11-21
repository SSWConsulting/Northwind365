using Northwind.Domain.Common;

namespace Northwind.Domain.UnitTests.Common;

public class CountryTests
{
    [Fact]
    public void IsAustralia_GivenAustralianCountry_ReturnsTrue()
    {
        // Arrange
        var country = new Country("Australia");

        // Act
        var result = country.IsAustralia;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAustralia_GivenNonAustralianCountry_ReturnsFalse()
    {
        // Arrange
        var country = new Country("New Zealand");

        // Act
        var result = country.IsAustralia;

        // Assert
        result.Should().BeFalse();
    }
}