using AutoFixture;
using AutoMapper;
using Common.Factories;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Customers;
using Northwind.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace Northwind.Application.UnitTests.Customers.Queries;

[Collection("QueryCollection")]
public class GetCustomerDetailQueryHandlerTests
{
    private readonly NorthwindDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerDetailQueryHandlerTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

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
        result.ShouldBeOfType<CustomerDetailVm>();
        result.Id.ShouldBe(customer.Id.Value);
    }
}