using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Application.Tests.Infrastructure;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.Application.Tests.Products.Commands
{
    public class CreateProductCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidCommand_InsertsNewProduct()
        {
            var command = new CreateProductCommand
            {
                CategoryId = 1,
                SupplierId = 1,
                ProductName = "Coffee",
                UnitPrice = 3.7m
            };

            var sut = new CreateProductCommandHandler(_context);

            var productId = await sut.Handle(command, CancellationToken.None);

            productId.ShouldNotBe(0);

            var product = _context.Products.Find(productId);

            product.ShouldNotBeNull();
        }
    } 
}
