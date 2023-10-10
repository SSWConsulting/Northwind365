using MediatR;
using Moq;
using Northwind.Application.Customers.Commands.CreateCustomer;
using System.Threading;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;

using Xunit;

namespace Northwind.Application.UnitTests.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandTests : CommandTestBase
{
    [Fact]
    public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var sut = new CreateCustomerCommandHandler(_context, mediatorMock.Object);
        var newCustomerId = new CustomerId(Guid.NewGuid());

        // Act
        var result = sut.Handle(new CreateCustomerCommand { Id = newCustomerId.Value }, CancellationToken.None);


        // Assert
        mediatorMock.Verify(m => m.Publish(It.Is<CustomerCreated>(cc => cc.CustomerId == newCustomerId), It.IsAny<CancellationToken>()), Times.Once);
    }
}