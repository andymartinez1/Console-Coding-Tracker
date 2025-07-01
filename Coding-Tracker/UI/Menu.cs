using Coding_Tracker.Controllers;
using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Coding_Tracker.Services;
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
                new SelectionPrompt<string>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(
                        Enums.MenuOptions.AddSession.GetDisplayName(),
                        Enums.MenuOptions.ViewSessions.GetDisplayName(),
                        Enums.MenuOptions.UpdateSession.GetDisplayName(),
                        Enums.MenuOptions.DeleteSession.GetDisplayName(),
                        Enums.MenuOptions.Quit.GetDisplayName()
                    )
            );

            switch (usersChoice)
            {
                case "Add Session":
                    AnsiConsole.Clear();
                    CodingController.AddSession();
                    break;
                case "View Sessions":
                    AnsiConsole.Clear();
                    var data = new DataConnection();
                    var sessions = data.GetAllSessions();
                    UserInterface.ViewSessions(sessions);
                    break;
                case "Update Session":
                    AnsiConsole.Clear();
                    CodingController.UpdateSession(0);
                    break;
                case "Delete Session":
                    AnsiConsole.Clear();
                    CodingController.DeleteSession(0);
                    break;
                case "Quit":
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
