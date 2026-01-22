using CodingTracker.Data;
using CodingTracker.Repository.CodingSessions;
using CodingTracker.Repository.Projects;
using CodingTracker.Services.CodingSessions;
using CodingTracker.Services.Projects;
using CodingTracker.Services.StopWatch;
using CodingTracker.Views;
using Microsoft.Extensions.DependencyInjection;

// Configuring the Dependency Injection container
var services = new ServiceCollection();

// Registering the dependencies
services.AddLogging();
services.AddDbContext<CodingDbContext>();
services.AddScoped<ISessionRepository, SessionRepository>();
services.AddScoped<ISessionService, SessionService>();
services.AddScoped<IProjectRepository, ProjectRepository>();
services.AddScoped<IProjectsService, ProjectsService>();
services.AddScoped<IStopWatchService, StopWatchService>();
services.AddScoped<Menu>();

// Building the service provider
var serviceProvider = services.BuildServiceProvider();

// Set up database
using (var scope = serviceProvider.CreateScope())
{
    var data = scope.ServiceProvider.GetRequiredService<CodingDbContext>();
    data.Database.EnsureDeleted();
    data.Database.EnsureCreated();
    SeedDatabase.SeedData(data);
}

// Get the main menu and run the app
var menu = serviceProvider.GetRequiredService<Menu>();
menu.MainMenu();

// Dispose of the service provider
serviceProvider.Dispose();
