using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.System.Commands.SeedSampleData;
using Northwind.Infrastructure;
using Northwind.Infrastructure.Identity;
using Northwind.Persistence;
using Northwind.Application;
using Northwind.WebUI.Common;
using Northwind.WebUI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseRegisteredServicesPage(app.Services);

    // Initialise and seed database
    using var scope = app.Services.CreateScope();

    // TODO: Update to use intializer
    //var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    //await initializer.InitializeAsync();
    //await initializer.SeedAsync();

    try
    {
        var northwindContext = scope.ServiceProvider.GetRequiredService<NorthwindDbContext>();
        northwindContext.Database.Migrate();

        var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        identityContext.Database.Migrate();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new SeedSampleDataCommand(), CancellationToken.None);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}
else
{
    app.UseExceptionHandler("/Error");


    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// TODO: Replace this with 'app.UseExceptionFilter();'
//app.UseCustomExceptionHandler();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseOpenApi();

//app.UseSwaggerUi3(settings => settings.DocumentPath = "/api/specification.json");
app.UseSwaggerUi3(settings => settings.Path = "/api");

app.UseRouting();

app.UseAuthentication();
// TODO: Fix and add back in
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
         "default",
         "{controller}/{action=Index}/{id?}");
app.MapControllers();
app.MapRazorPages();

app.MapCategoryEndpoints();
app.MapCustomerEndpoints();
app.MapIdentityEndpoints();
app.MapProductEndpoints();

app.UseSpa(spa =>
{
    // To learn more about options for serving an Angular SPA from ASP.NET Core,
    // see https://go.microsoft.com/fwlink/?linkid=864501

    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

app.Run();

public partial class Program { }