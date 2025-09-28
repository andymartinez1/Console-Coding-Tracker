using CodingTracker.Enums;
using CodingTracker.Services;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Views;

public class Menu
{
    private readonly ICodingService _codingService;

    private readonly SessionMenuOptions[] _menuOptions =
    [
        SessionMenuOptions.ViewAllSessions,
        SessionMenuOptions.ViewSession,
        SessionMenuOptions.AddSession,
        SessionMenuOptions.UpdateSession,
        SessionMenuOptions.DeleteSession,
        SessionMenuOptions.BackToMainMenu,
    ];

    public Menu(ICodingService codingService)
    {
        _codingService = codingService;
    }

    public void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<SessionMenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_menuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );
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
                    .AddChoices(_menuOptions)
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
                    AnsiConsole.MarkupLine(
                        "[blue]Thank you for using this coding tracker! Press any key to exit. Goodbye![/]"
                    );
                    Console.ReadKey();
                    isMenuRunning = false;
                    Environment.Exit(0);
                    break;
                default:
                    AnsiConsole.Clear();
                    Console.WriteLine("Invalid choice. Please choose one of the above");
                    break;
            }
        }
    }
}
