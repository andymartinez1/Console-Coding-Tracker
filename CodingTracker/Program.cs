using CodingTracker.Data;
using CodingTracker.Repository.CodingSessions;
using CodingTracker.Repository.ProgrammingLanguages;
using CodingTracker.Repository.Projects;
using CodingTracker.Services.CodingSessions;
using CodingTracker.Services.ProgrammingLanguages;
using CodingTracker.Services.Projects;
using CodingTracker.Services.StopWatch;
using CodingTracker.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// Configuring the Dependency Injection container
var services = new ServiceCollection();

// Registering the dependencies
services.AddDbContext<CodingDbContext>();
services.AddScoped<ICodingRepository, CodingRepository>();
services.AddScoped<IProjectRepository, ProjectRepository>();
services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>();
services.AddScoped<ICodingService, CodingService>();
services.AddScoped<IProjectsService, ProjectsService>();
services.AddScoped<IProgrammingLanguagesService, ProgrammingLanguagesService>();
services.AddScoped<IStopWatchService, StopWatchService>();
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
