using AutoMapper;
using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.Application.UnitTests.Common;
using Northwind.Infrastructure.Persistence;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Queries;

[Collection(QueryCollection.Definition)]
public class GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
{
    private readonly NorthwindDbContext _context = fixture.Context;
    private readonly IMapper _mapper = fixture.Mapper;

    [Fact]
    public async Task GetCustomersTest()
    {
        // Arrange
        var sut = new GetCustomersListQueryHandler(_context, _mapper);

        // Act
        var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<CustomersListVm>();
        result.Customers.Count.Should().Be(4);
    }
}