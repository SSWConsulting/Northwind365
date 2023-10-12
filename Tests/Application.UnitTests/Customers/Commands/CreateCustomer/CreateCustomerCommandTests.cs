using AutoFixture;
using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandTests : CommandTestBase
{
    [Fact]
    //[AutoData]
    public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var sut = new CreateCustomerCommandHandler(_context, mediatorMock.Object);
        var newCustomerId = new CustomerId("123");

        var fixture = new Fixture();
        var command = fixture.Build<CreateCustomerCommand>()
            .With(x => x.ContactName, "ContactName")
            .With(x => x.ContactTitle, "ContactTitle")
            .With(x => x.Id, newCustomerId.Value)
            .Create();

        // Act
        var result = sut.Handle(command, CancellationToken.None);

        // Assert
        mediatorMock.Verify(
            m => m.Publish(It.Is<CustomerCreated>(cc => cc.CustomerId == newCustomerId), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}