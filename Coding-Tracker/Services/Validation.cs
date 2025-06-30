using System.Globalization;
using Coding_Tracker.Models;
using Coding_Tracker.UI;
using Spectre.Console;

namespace Coding_Tracker.Services;

public class Validation
{
    public static DateTime[] ValidateDateTime()
    {
        DateTime startDate;
        DateTime endDate;

        var startDateInput = AnsiConsole.Ask<string>("Enter the start date (MM-dd-yyyy hh:mm ):");
        if (startDateInput == null)
            Menu.MainMenu();

        while (
            !DateTime.TryParseExact(
                startDateInput,
                "MM-dd-yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDate
            )
        )
            startDateInput = AnsiConsole.Ask<string>(
                "\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n"
            );

        var endDateInput = AnsiConsole.Ask<string>("Enter the end date (MM-dd-yyyy hh:mm):");
        if (endDateInput == null)
            Menu.MainMenu();

        while (
            !DateTime.TryParseExact(
                endDateInput,
                "MM-dd-yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out endDate
            )
        )
            endDateInput = AnsiConsole.Ask<string>(
                "\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n"
            );

        while (startDate > endDate)
        {
            endDateInput = AnsiConsole.Ask<string>(
                "\n\nEnd date can't be before start date. Please try again\n\n"
            );

            while (
                !DateTime.TryParseExact(
                    endDateInput,
                    "MM-dd-yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out endDate
                )
            )
                endDateInput = AnsiConsole.Ask<string>(
                    "\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n"
                );
        }

        return [startDate, endDate];
    }

    public static void ValidateSessionsList(List<CodingSession> sessions)
    {
        if (sessions.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No sessions found![/]");
            AnsiConsole.MarkupLine("Please add a session first.");
            Menu.MainMenu();
        }
    }
}
