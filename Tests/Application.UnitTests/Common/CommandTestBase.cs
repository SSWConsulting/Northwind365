using Northwind.Infrastructure.Persistence;

namespace Northwind.Application.UnitTests.Common;

public class CommandTestBase : IDisposable
{
    protected readonly NorthwindDbContext _context = NorthwindContextFactory.Create();

    public void Dispose()
    {
        NorthwindContextFactory.Destroy(_context);
    }
}