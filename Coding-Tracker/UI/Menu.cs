using Coding_Tracker.Controllers;
using Coding_Tracker.Data;
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
                       "Add Coding Session",
                       "View All Coding Sessions",
                       "Update Coding Session",
                       "Delete Coding Session",
                       "Quit")
                    );

            switch (usersChoice)
            {
                case "Add Coding Session":
                    AnsiConsole.Clear();
                    SessionController.AddSession();
                    break;
                case "View All Coding Sessions":
                    AnsiConsole.Clear();
                    var data = new DataConnection();
                    var sessions = data.GetAllSessions();
                    SessionController.ViewSessions(sessions);
                    break;
                case "Update Coding Session":
                    AnsiConsole.Clear();
                    SessionController.UpdateSession(0);
                    break;
                case "Delete Coding Session":
                    AnsiConsole.Clear();
                    SessionController.DeleteSession(0);
                    break;
                case "Quit":
                    AnsiConsole.Clear();
                    Console.WriteLine("Goodbye");
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
