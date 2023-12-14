using Northwind.Application;
using Northwind.Infrastructure;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI;
using Northwind.WebUI.Features;
using Northwind.WebUI.Filters;
using SSW.CleanArchitecture.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app
    .MapApiGroup("auth")
    .MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseRegisteredServicesPage(app.Services);

    using var scope = app.Services.CreateScope();

    try
    {
        var identityInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        if (await identityInitializer.CanConnect())
        {
            //await identityInitializer.EnsureDeleted();
            await identityInitializer.InitializeAsync();
        }

        var initializer = scope.ServiceProvider.GetRequiredService<NorthwindDbContextInitializer>();
        if (await identityInitializer.CanConnect())
        {
            await initializer.InitializeAsync();
            await initializer.SeedAsync();
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database");
    }
}
else
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionFilter();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();

app.UseSwaggerUi(settings => settings.Path = "/api");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapRazorPages();

app.MapCategoryEndpoints();
app.MapCustomerEndpoints();
app.MapProductEndpoints();

app.MapFallbackToFile("index.html");

app.Run();