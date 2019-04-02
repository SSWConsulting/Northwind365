using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Application.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace Northwind.Application.Tests.Products.Commands
{
    public class CreateProductCommandTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_InsertsNewProduct()
        {
        }
    }
}
