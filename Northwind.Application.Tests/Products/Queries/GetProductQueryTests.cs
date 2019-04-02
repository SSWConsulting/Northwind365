using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Application.Products.Queries.GetProduct;
using Northwind.Application.Tests.Infrastructure;
using Northwind.Persistence;
using Shouldly;
using Xunit;

namespace Northwind.Application.Tests.Products.Queries
{
    public class GetProductQueryTests
    {
        [Fact]
        public async Task Handle_GivenValidId_ReturnsCorrectProduct()
        {
        }
    }
}
