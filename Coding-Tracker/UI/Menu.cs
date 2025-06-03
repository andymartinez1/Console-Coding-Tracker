using Coding_Tracker.Controllers;
using Coding_Tracker.Data;
using Spectre.Console;

namespace Coding_Tracker.UI
{
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
                           "View Coding Sessions",
                           "Update Coding Session",
                           "Delete Coding Session",                        
                           "Quit")
                        );

                switch (usersChoice)
                {
                    case "Add Coding Session":
                        SessionController.AddSession();
                        break;
                    case "View Coding Sessions":
                        var data = new DataConnection();
                        var sessions = data.GetSessions();
                        SessionController.ViewSessions(sessions);
                        break;
                    case "Update Coding Session":
                        break;
                    case "Delete Coding Session":
                        break;
                    case "Quit":
                        Console.WriteLine("Goodbye");
                        isMenuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose one of the above");
                        break;
                }
            }
        }
    }
}
