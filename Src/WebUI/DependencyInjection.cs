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

        services
            .AddControllersWithViews()
            .AddNewtonsoftJson();
        
        services.AddFluentValidationAutoValidation();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }
}