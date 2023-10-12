using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Application.UnitTests.Common;
using Xunit;
using AutoMapper;

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
        var customerId = "DANIEL";
        var sut = new GetCustomerDetailQueryHandler(_context, _mapper);

        // Act
        var result = await sut.Handle(new GetCustomerDetailQuery(customerId), CancellationToken.None);

        // Assert
        result.ShouldBeOfType<CustomerDetailVm>();
        result.Id.ShouldBe(customerId);
    }
}