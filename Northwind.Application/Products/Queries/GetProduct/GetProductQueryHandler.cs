using Microsoft.EntityFrameworkCore;
using Northwind.Application.Exceptions;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Application.Interfaces;

namespace Northwind.Application.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : MediatR.IRequestHandler<GetProductQuery, ProductViewModel>
    {
        private readonly INorthwindDbContext _context;

        public GetProductQueryHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == request.Id)
                .Select(ProductViewModel.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            product.EditEnabled = true;
            product.DeleteEnabled = false;

            return product;
        }
    }
}
