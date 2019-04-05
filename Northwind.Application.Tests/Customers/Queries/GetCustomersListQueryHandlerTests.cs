using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.Application.Interfaces;
using Northwind.Application.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace Northwind.Application.Tests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomersListQueryHandlerTests
    {
        private readonly INorthwindDbContext _context;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            //var sut = new GetCustomersListQueryHandler(_context, _mapper);

            //var result = await sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            //result.ShouldBeOfType<CustomersListViewModel>();

            //result.Customers.Count.ShouldBe(3);
        }
    }
}