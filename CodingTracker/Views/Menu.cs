using CodingTracker.Enums;
using CodingTracker.Services;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Views;

public class Menu
{
    private readonly ICodingService _codingService;
    private readonly IProjectsService _projectsService;
    private readonly IProgrammingLanguagesService _programmingLanguagesService;
    private readonly IStopWatchService _stopWatchService;

    private readonly SessionMenuOptions[] _sessionMenuOptions =
    [
        SessionMenuOptions.ViewAllSessions,
        SessionMenuOptions.ViewSession,
        SessionMenuOptions.AddSession,
        SessionMenuOptions.UpdateSession,
        SessionMenuOptions.DeleteSession,
        SessionMenuOptions.BackToMainMenu,
    ];

    private readonly MainMenuOptions[] _mainMenuOptions =
    [
        MainMenuOptions.CodingSessionMenu,
        MainMenuOptions.ProjectMenu,
        MainMenuOptions.ProgrammingLanguageMenu,
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

    private readonly ProgrammingLanguageMenuOptions[] _languageMenuOptions =
    [
        ProgrammingLanguageMenuOptions.AddProgrammingLanguage,
        ProgrammingLanguageMenuOptions.ViewAllProgrammingLanguages,
        ProgrammingLanguageMenuOptions.ViewProgrammingLanguage,
        ProgrammingLanguageMenuOptions.UpdateProgrammingLanguage,
        ProgrammingLanguageMenuOptions.DeleteProgrammingLanguage,
        ProgrammingLanguageMenuOptions.BackToMainMenu,
    ];

    public Menu(
        ICodingService codingService,
        IProjectsService projectsService,
        IProgrammingLanguagesService programmingLanguagesService,
        IStopWatchService stopWatchService
    )
    {
        _codingService = codingService;
        _projectsService = projectsService;
        _programmingLanguagesService = programmingLanguagesService;
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
                case MainMenuOptions.ProgrammingLanguageMenu:
                    AnsiConsole.Clear();
                    LanguagesMenu();
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

    private void LanguagesMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<ProgrammingLanguageMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_languageMenuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case ProgrammingLanguageMenuOptions.AddProgrammingLanguage:
                    break;
                case ProgrammingLanguageMenuOptions.ViewAllProgrammingLanguages:
                    break;
                case ProgrammingLanguageMenuOptions.ViewProgrammingLanguage:
                    break;
                case ProgrammingLanguageMenuOptions.UpdateProgrammingLanguage:
                    break;
                case ProgrammingLanguageMenuOptions.DeleteProgrammingLanguage:
                    break;
                case ProgrammingLanguageMenuOptions.BackToMainMenu:
                    AnsiConsole.Clear();
                    MainMenu();
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
                    break;
                case ProjectMenuOptions.ViewAllProjects:
                    break;
                case ProjectMenuOptions.ViewProject:
                    break;
                case ProjectMenuOptions.UpdateProject:
                    break;
                case ProjectMenuOptions.DeleteProject:
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
                case SessionMenuOptions.AddSession:
                    AnsiConsole.Clear();
                    _codingService.AddSession();
                    break;
                case SessionMenuOptions.ViewAllSessions:
                    AnsiConsole.Clear();
                    _codingService.GetAllSessions();
                    break;
                case SessionMenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    _codingService.UpdateSession();
                    break;
                case SessionMenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    _codingService.DeleteSession();
                    break;
                case SessionMenuOptions.BackToMainMenu:
                    AnsiConsole.Clear();
                    MainMenu();
                    break;
            }
        }
    }
}
