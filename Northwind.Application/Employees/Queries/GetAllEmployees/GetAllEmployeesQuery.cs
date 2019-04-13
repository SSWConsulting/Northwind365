using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Interfaces;

namespace Northwind.Application.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<EmployeeListVm>
    {
        public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, EmployeeListVm>
        {
            private readonly INorthwindDbContext _context;

            public GetAllEmployeesQueryHandler(INorthwindDbContext context)
            {
                _context = context;
            }

            public async Task<EmployeeListVm> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees
                    .Select(EmployeeListItem.Projection)
                    .ToListAsync(cancellationToken);

                return new EmployeeListVm
                {
                    Employees = employees
                };
            }
        }
    }
}
