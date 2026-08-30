using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Enums;
using CodingTracker.Services;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Views;

public class Menu
{
    private readonly MainMenuOptions[] _mainMenuOptions =
    [
        MainMenuOptions.CodingSessionMenu,
        MainMenuOptions.ProjectMenu,
        MainMenuOptions.Exit
    ];

    private readonly ProjectMenuOptions[] _projectMenuOptions =
    [
        ProjectMenuOptions.AddProject,
        ProjectMenuOptions.ViewAllProjects,
        ProjectMenuOptions.ViewProject,
        ProjectMenuOptions.UpdateProject,
        ProjectMenuOptions.DeleteProject,
        ProjectMenuOptions.BackToMainMenu
    ];

    private readonly IProjectsService _projectsService;

    private readonly SessionMenuOptions[] _sessionMenuOptions =
    [
        SessionMenuOptions.StartSession,
        SessionMenuOptions.AddSession,
        SessionMenuOptions.ViewAllSessions,
        SessionMenuOptions.ViewSession,
        SessionMenuOptions.UpdateSession,
        SessionMenuOptions.DeleteSession,
        SessionMenuOptions.BackToMainMenu
    ];

    private readonly ISessionService _sessionService;

    private readonly StopWatchMenuOptions[] _stopWatchMenuOptions =
    [
        StopWatchMenuOptions.StartTimedSession,
        StopWatchMenuOptions.StopTimedSession,
        StopWatchMenuOptions.BackToSessionsMenu
    ];


    public Menu(
        ISessionService sessionService,
        IProjectsService projectsService
    )
    {
        _sessionService = sessionService;
        _projectsService = projectsService;
    }

    public void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_mainMenuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case MainMenuOptions.CodingSessionMenu:
                    AnsiConsole.Clear();
                    SessionsMenu();
                    break;
                case MainMenuOptions.ProjectMenu:
                    AnsiConsole.Clear();
                    ProjectsMenu();
                    break;
                case MainMenuOptions.Exit:
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine(
                        "[blue]Thank you for using this coding tracker! Press any key to exit. Goodbye![/]"
                    );
                    Console.ReadKey();
                    isMenuRunning = false;
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void ProjectsMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<ProjectMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_projectMenuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case ProjectMenuOptions.AddProject:
                    AnsiConsole.Clear();
                    _projectsService.AddAsync(new AddProjectRequest());
                    break;
                case ProjectMenuOptions.ViewAllProjects:
                    AnsiConsole.Clear();
                    _projectsService.GetAllAsync();
                    break;
                case ProjectMenuOptions.ViewProject:
                    AnsiConsole.Clear();
                    _projectsService.GetByIdAsync(0);
                    break;
                case ProjectMenuOptions.UpdateProject:
                    AnsiConsole.Clear();
                    _projectsService.UpdateAsync(null);
                    break;
                case ProjectMenuOptions.DeleteProject:
                    AnsiConsole.Clear();
                    _projectsService.DeleteAsync(0);
                    break;
                case ProjectMenuOptions.BackToMainMenu:
                    AnsiConsole.Clear();
                    MainMenu();
                    break;
            }
        }
    }

    public void SessionsMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<SessionMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_sessionMenuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case SessionMenuOptions.StartSession:
                    AnsiConsole.Clear();
                    StopWatchMenu();
                    break;
                case SessionMenuOptions.AddSession:
                    AnsiConsole.Clear();
                    _sessionService.AddAsync(new AddSessionRequest());
                    break;
                case SessionMenuOptions.ViewAllSessions:
                    AnsiConsole.Clear();
                    _sessionService.GetAllAsync();
                    break;
                case SessionMenuOptions.ViewSession:
                    AnsiConsole.Clear();
                    _sessionService.GetByIdAsync(0);
                    break;
                case SessionMenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    _sessionService.UpdateAsync(null);
                    break;
                case SessionMenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    _sessionService.DeleteAsync(0);
                    break;
                case SessionMenuOptions.BackToMainMenu:
                    AnsiConsole.Clear();
                    MainMenu();
                    break;
            }
        }
    }

    public void StopWatchMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<StopWatchMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_stopWatchMenuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case StopWatchMenuOptions.StartTimedSession:
                    AnsiConsole.Clear();
                    UserInterface.ViewStopWatchTimer(_sessionService);
                    break;
                case StopWatchMenuOptions.StopTimedSession:
                    AnsiConsole.Clear();

                    if (!_sessionService.IsStopwatchRunning())
                    {
                        AnsiConsole.MarkupLine("[yellow]No timer is currently running.[/]");
                        break;
                    }

                    _sessionService.StopTimer();
                    _sessionService.AddStopWatchSessionAsync();
                    break;
                case StopWatchMenuOptions.BackToSessionsMenu:
                    AnsiConsole.Clear();
                    SessionsMenu();
                    break;
            }
        }
    }
}