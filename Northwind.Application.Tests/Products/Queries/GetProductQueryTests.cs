using Northwind.Application.Interfaces;
using Northwind.Application.Products.Queries.GetProduct;
using Northwind.Application.Tests.Infrastructure;
using Northwind.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.Application.Tests.Products.Queries
{
    [Collection("QueryCollection")]
    public class GetProductQueryTests
    {
        private readonly INorthwindDbContext _context;

        public GetProductQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsCorrectProduct()
        {
            var query = new GetProductQuery { Id = 999 };

            var sut = new GetProductQueryHandler(_context);

            var result = await sut.Handle(query, CancellationToken.None);

            result.ProductId.ShouldBe(999);
        }
    }
}
