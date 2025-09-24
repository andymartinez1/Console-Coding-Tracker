using CodingTracker.Controllers;
using CodingTracker.Enums;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Views;

public class Menu
{
    private readonly CodingController _codingController;

    private readonly SessionMenuOptions[] _menuOptions =
    [
        SessionMenuOptions.ViewAllSessions,
        SessionMenuOptions.ViewSession,
        SessionMenuOptions.AddSession,
        SessionMenuOptions.UpdateSession,
        SessionMenuOptions.DeleteSession,
        SessionMenuOptions.Quit,
    ];

    public Menu(CodingController codingController)
    {
        _codingController = codingController;
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

            switch (usersChoice)
            {
                case SessionMenuOptions.AddSession:
                    AnsiConsole.Clear();
                    _codingController.AddSession();
                    break;
                case SessionMenuOptions.ViewAllSessions:
                    AnsiConsole.Clear();
                    _codingController.GetAllSessions();
                    break;
                case SessionMenuOptions.ViewSession:
                    AnsiConsole.Clear();
                    _codingController.GetSession();
                    break;
                case SessionMenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    _codingController.UpdateSession();
                    break;
                case SessionMenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    _codingController.DeleteSession();
                    break;
                case SessionMenuOptions.Quit:
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
