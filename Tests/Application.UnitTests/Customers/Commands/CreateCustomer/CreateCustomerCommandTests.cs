using AutoFixture;
using MediatR;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;
using Northwind.Infrastructure.Persistence;
using NSubstitute;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandTests : IDisposable, IAsyncDisposable
{
    private readonly NorthwindDbContext _context = NorthwindContextFactory.Create();

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
    {
        // Arrange
        var sut = new CreateCustomerCommandHandler(_context);
        var newCustomerId = new CustomerId("123");

        var fixture = new Fixture();
        var command = fixture.Build<CreateCustomerCommand>()
            .With(x => x.ContactName, "ContactName")
            .With(x => x.ContactTitle, "ContactTitle")
            .With(x => x.Phone, "123456")
            .With(x => x.Fax, "123456")
            .With(x => x.PostalCode, "4000")
            .With(x => x.Country, "New Zealand")
            .With(x => x.Id, newCustomerId.Value)
            .Create();

        // Act
        await sut.Handle(command, CancellationToken.None);

        // Assert
        await mediatorMock.Received()
            .Publish(Arg.Is<CustomerCreatedEvent>(cc => cc.CustomerId == newCustomerId), Arg.Any<CancellationToken>());
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}