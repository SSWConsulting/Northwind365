using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Northwind.Infrastructure.Identity;

// NOTE: Workaround as per https://github.com/dotnet/aspnetcore/issues/46025#issuecomment-1379085253

// public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
// {
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//         : base(options)
//     {
//     }
//
//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         base.OnModelCreating(builder);
//         // Customize the ASP.NET Identity model and override the defaults if needed.
//         // For example, you can rename the ASP.NET Identity table names and more.
//         // Add your customizations after calling base.OnModelCreating(builder);
//     }
// }

// public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
// {
//     public ApplicationDbContext(
//         DbContextOptions<ApplicationDbContext> options,
//         IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
//     {
//     }

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IPersistedGrantDbContext
{
    private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options)
    {
        _operationalStoreOptions = operationalStoreOptions;
    }

    public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;

    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = null!;

    public DbSet<Key> Keys { get; set; } = null!;

    public DbSet<ServerSideSession> ServerSideSessions { get; set; } = null!;

    Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
    }
}