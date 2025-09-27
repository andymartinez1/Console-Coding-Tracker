using System.Data;
using CodingTracker.Data;
using CodingTracker.Repository;
using CodingTracker.Services;
using CodingTracker.Views;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Configuring the Dependency Injection container
var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString =
    configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException();

// Registering the dependencies
services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));
services.AddScoped<CodingDbContext>();
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
