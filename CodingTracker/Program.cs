using CodingTracker.Data;
using CodingTracker.Repository;
using CodingTracker.Repository.CodingSessions;
using CodingTracker.Services;
using CodingTracker.Services.CodingSessions;
using CodingTracker.Views;
using Microsoft.Extensions.DependencyInjection;

// Configuring the Dependency Injection container
var services = new ServiceCollection();

// Registering the dependencies
services.AddDbContext<CodingDbContext>();
services.AddScoped<ICodingRepository, CodingRepository>();
services.AddScoped<ICodingService, CodingService>();
services.AddScoped<Menu>();

// Building the service provider
var serviceProvider = services.BuildServiceProvider();

// Set up database
using (var scope = serviceProvider.CreateScope())
{
    var data = scope.ServiceProvider.GetRequiredService<CodingDbContext>();
}

// Get the main menu and run the app
var menu = serviceProvider.GetRequiredService<Menu>();
menu.MainMenu();

// Dispose of the service provider
serviceProvider.Dispose();
