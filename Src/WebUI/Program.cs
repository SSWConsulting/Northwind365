using Northwind.Application;
using Northwind.Infrastructure;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.Persistence;
using Northwind.WebUI;
using Northwind.WebUI.Common;
using Northwind.WebUI.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseRegisteredServicesPage(app.Services);

    using var scope = app.Services.CreateScope();

    try
    {

        var identityInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        //await identityInitializer.EnsureDeleted();
        await identityInitializer.InitializeAsync();

        // Initialise and seed database
        var initializer = scope.ServiceProvider.GetRequiredService<NorthwindDbContextInitializer>();
        await initializer.InitializeAsync();
        await initializer.SeedAsync();
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

app.UseCustomExceptionHandler();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseOpenApi();
// app.UseSwaggerUi3(settings => settings.Path = "/api");

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

// TODO: Are controllers needed?
// app.MapControllerRoute(
//          "default",
//          "{controller}/{action=Index}/{id?}");
//app.MapControllers();
app.MapRazorPages();

app.MapCategoryEndpoints();
app.MapCustomerEndpoints();
app.MapIdentityEndpoints();
app.MapProductEndpoints();

app.MapFallbackToFile("index.html");

app.Run();