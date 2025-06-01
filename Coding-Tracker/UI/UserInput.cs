using Spectre.Console;

namespace Coding_Tracker.UI
{
    internal class UserInput
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
                           "Add Habit",
                           "Delete Habit",
                           "Update Habit",
                           "Add Progress",
                           "Delete Progress",
                           "View All Progress",
                           "Update Progress",
                           "Quit")
                        );

                switch (usersChoice)
                {
                    case "Add Habit":
                        break;
                    case "Delete Habit":
                        break;
                    case "Update Habit":
                        break;
                    case "Add Progress":
                        break;
                    case "Delete Progress":
                        break;
                    case "View All Progress":
                        break;
                    case "Update Progress":
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
