using System.Globalization;
using Coding_Tracker.Models;
using Coding_Tracker.UI;
using Spectre.Console;

namespace Coding_Tracker.Services;

public class Validation
{
    public static bool IsValidDate(string date, string format)
    {
        if (
            !DateTime.TryParseExact(
                date,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
            return false;

        return true;
    }

    public static bool IsStartDateBeforeEndDate(string startDate, string endDate)
    {
        var start = DateTime.ParseExact(
            startDate,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );
        var end = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

        return start <= end;
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
