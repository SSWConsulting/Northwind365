using AutoFixture;
using MediatR;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;
using NSubstitute;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandTests : CommandTestBase
{
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Arrange
        var mediatorMock = Substitute.For<IMediator>();
        var sut = new CreateCustomerCommandHandler(_context, mediatorMock);
        var newCustomerId = new CustomerId("123");

        var fixture = new Fixture();
        var command = fixture.Build<CreateCustomerCommand>()
            .With(x => x.ContactName, "ContactName")
            .With(x => x.ContactTitle, "ContactTitle")
            .With(x => x.Id, newCustomerId.Value)
            .Create();

        // Act
        await sut.Handle(command, CancellationToken.None);

        // Assert
        await mediatorMock.Received()
            .Publish(Arg.Is<CustomerCreatedEvent>(cc => cc.CustomerId == newCustomerId), Arg.Any<CancellationToken>());
    }
}