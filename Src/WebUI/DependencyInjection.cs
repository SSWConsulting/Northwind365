using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI.Services;

namespace Northwind.WebUI;

public static class DependencyInjection
{
    public static void AddWebUI(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHealthChecks()
            .AddDbContextCheck<NorthwindDbContext>();

        services.AddOpenApiDocument(configure => configure.Title = "Northwind Traders API");
        services.AddEndpointsApiExplorer();

        // NOTE: This will be removed soon
#pragma warning disable CS0618 // Type or member is obsolete
        services
            .AddControllersWithViews()
            .AddNewtonsoftJson()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<INorthwindDbContext>());
#pragma warning restore CS0618 // Type or member is obsolete

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
}