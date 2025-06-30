using Coding_Tracker.Controllers;
using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Spectre.Console;

namespace Coding_Tracker.UI;

internal class Menu
{
    internal static void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(
                        MenuOptions.AddSession,
                        MenuOptions.ViewSessions,
                        MenuOptions.UpdateSession,
                        MenuOptions.DeleteSession,
                        MenuOptions.Quit
                    )
            );

            switch (usersChoice)
            {
                case MenuOptions.AddSession:
                    AnsiConsole.Clear();
                    CodingController.AddSession();
                    break;
                case MenuOptions.ViewSessions:
                    AnsiConsole.Clear();
                    var data = new DataConnection();
                    var sessions = data.GetAllSessions();
                    UserInterface.ViewSessions(sessions);
                    break;
                case MenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    CodingController.UpdateSession(0);
                    break;
                case MenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    CodingController.DeleteSession(0);
                    break;
                case MenuOptions.Quit:
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine(
                        "[red]Thank you for using this coding tracker! Press any key to exit. Goodbye![/]"
                    );
                    Console.ReadKey();
                    isMenuRunning = false;
                    break;
                default:
                    AnsiConsole.Clear();
                    Console.WriteLine("Invalid choice. Please choose one of the above");
                    break;
            }
        }
    }
}
