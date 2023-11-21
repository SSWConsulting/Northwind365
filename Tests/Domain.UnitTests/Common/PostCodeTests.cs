using Northwind.Domain.Common;

namespace Northwind.Domain.UnitTests.Common;

public class PostCodeTests
{
    [Fact]
    public void IsQueenslandPostCode_GivenQLDPostCode_ReturnsTrue()
    {
        // Arrange
        var postCode = new PostCode("4000");

        // Act
        var result = postCode.IsQueenslandPostCode;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsQueenslandPostCode_GivenNonQLDPostCode_ReturnsFalse()
    {
        // Arrange
        var postCode = new PostCode("2000");

        // Act
        var result = postCode.IsQueenslandPostCode;

        // Assert
        result.Should().BeFalse();
    }
}