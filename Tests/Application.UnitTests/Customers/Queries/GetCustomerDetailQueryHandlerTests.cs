using AutoMapper;
using Common.Factories;
using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.UnitTests.Common;
using Northwind.Infrastructure.Persistence;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Queries;

[Collection("QueryCollection")]
public class GetCustomerDetailQueryHandlerTests(QueryTestFixture fixture)
{
    private readonly NorthwindDbContext _context = fixture.Context;
    private readonly IMapper _mapper = fixture.Mapper;

    [Fact]
    public async Task GetCustomerDetail()
    {
        // Arrange
        var customer = CustomerFactory.Generate();
        _context.Customers.AddRange(customer);
        await _context.SaveChangesAsync();

        var sut = new GetCustomerDetailQueryHandler(_context, _mapper);
        var query = new GetCustomerDetailQuery(customer.Id.Value);

        // Act
        var result = await sut.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CustomerDetailVm>();
        result.Id.Should().Be(customer.Id.Value);
    }
}