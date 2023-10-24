using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Infrastructure.Identity;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//, IPersistedGrantDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}