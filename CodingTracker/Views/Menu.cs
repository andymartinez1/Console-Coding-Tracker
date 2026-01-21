using CodingTracker.Enums;
using CodingTracker.Services.CodingSessions;
using CodingTracker.Services.Projects;
using CodingTracker.Services.StopWatch;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Views;

public class Menu
{
    private readonly MainMenuOptions[] _mainMenuOptions =
    [
        MainMenuOptions.CodingSessionMenu,
        MainMenuOptions.ProjectMenu,
        MainMenuOptions.Exit,
    ];

    private readonly ProjectMenuOptions[] _projectMenuOptions =
    [
        ProjectMenuOptions.AddProject,
        ProjectMenuOptions.ViewAllProjects,
        ProjectMenuOptions.ViewProject,
        ProjectMenuOptions.UpdateProject,
        ProjectMenuOptions.DeleteProject,
        ProjectMenuOptions.BackToMainMenu,
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
        SessionMenuOptions.BackToMainMenu,
    ];

    private readonly ISessionService _sessionService;

    private readonly StopWatchMenuOptions[] _stopWatchMenuOptions =
    [
        StopWatchMenuOptions.Start,
        StopWatchMenuOptions.Stop,
        StopWatchMenuOptions.ShowElapsedTime,
        StopWatchMenuOptions.Reset,
        StopWatchMenuOptions.BackToSessionsMenu,
    ];

    private readonly IStopWatchService _stopWatchService;

    public Menu(
        ISessionService sessionService,
        IProjectsService projectsService,
        IStopWatchService stopWatchService
    )
    {
        _sessionService = sessionService;
        _projectsService = projectsService;
        _stopWatchService = stopWatchService;
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
                    _projectsService.AddProject();
                    break;
                case ProjectMenuOptions.ViewAllProjects:
                    AnsiConsole.Clear();
                    _projectsService.GetAllProjects();
                    break;
                case ProjectMenuOptions.ViewProject:
                    AnsiConsole.Clear();
                    _projectsService.ViewProjectById();
                    break;
                case ProjectMenuOptions.UpdateProject:
                    AnsiConsole.Clear();
                    _projectsService.UpdateProject();
                    break;
                case ProjectMenuOptions.DeleteProject:
                    AnsiConsole.Clear();
                    _projectsService.DeleteProject();
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
                    _sessionService.AddSession();
                    break;
                case SessionMenuOptions.ViewAllSessions:
                    AnsiConsole.Clear();
                    _sessionService.GetAllSessions();
                    break;
                case SessionMenuOptions.ViewSession:
                    AnsiConsole.Clear();
                    _sessionService.ViewSessionById();
                    break;
                case SessionMenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    _sessionService.UpdateSession();
                    break;
                case SessionMenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    _sessionService.DeleteSession();
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
                case StopWatchMenuOptions.Start:
                    AnsiConsole.Clear();
                    _stopWatchService.StartTimer();
                    break;
                case StopWatchMenuOptions.Stop:
                    AnsiConsole.Clear();
                    _stopWatchService.StopTimer();
                    break;
                case StopWatchMenuOptions.BackToSessionsMenu:
                    AnsiConsole.Clear();
                    SessionsMenu();
                    break;
            }
        }
    }
}
