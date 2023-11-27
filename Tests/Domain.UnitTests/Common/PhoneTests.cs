using Northwind.Domain.Customers;

namespace Northwind.Domain.UnitTests.Common;

public class PhoneTests
{
    [Fact]
    public void IsQueenslandLandline_GivenQLDNumber_ReturnsTrue()
    {
        // Arrange
        var phone = new Phone("0733333333");

        // Act
        var result = phone.IsQueenslandLandLine;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsQueenslandLandline_GivenNonQLDNumber_ReturnsFalse()
    {
        // Arrange
        var phone = new Phone("0833333333");

        // Act
        var result = phone.IsQueenslandLandLine;

        // Assert
        result.Should().BeFalse();
    }
}