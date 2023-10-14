using AutoMapper;
using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.Application.UnitTests.Common;
using Northwind.Infrastructure.Persistence;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Queries;

[Collection("QueryCollection")]
public class GetCustomersListQueryHandlerTests
{
    private readonly NorthwindDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetCustomersTest()
    {
        var sut = new GetCustomersListQueryHandler(_context, _mapper);

        var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

        result.Should().BeOfType<CustomersListVm>();

        result.Customers.Count.Should().Be(4);
    }
}