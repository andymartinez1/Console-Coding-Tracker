using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Coding_Tracker.UI;
using Spectre.Console;
using System.Globalization;

namespace Coding_Tracker.Controllers;

internal class SessionController
{

    internal static DateTime[] GetDates()
    {
        DateTime startDate;
        DateTime endDate;
        var startDateInput = AnsiConsole.Ask<string>("Enter the start date (MM-dd-yyyy hh:mm ):");
        var endDateInput = AnsiConsole.Ask<string>("Enter the end date (MM-dd-yyyy hh:mm):");

        if (startDateInput == null) Menu.MainMenu();

        while (!DateTime.TryParseExact(startDateInput, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
        {
            startDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n");
        }

        if (endDateInput == null) Menu.MainMenu();

        while (!DateTime.TryParseExact(endDateInput, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n");
        }

        while (startDate > endDate)
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nEnd date can't be before start date. Please try again\n\n");

            while (!DateTime.TryParseExact(endDateInput, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n");
            }
        }

        return [startDate, endDate];
    }

    internal static void AddSession()
    {
        CodingSession session = new();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        if (session.ProjectName == null) Menu.MainMenu();

        var Dates = GetDates();
        session.StartTime = Dates[0];
        session.EndTime = Dates[1];

        var data = new DataConnection();
        data.InsertSession(session);
    }

    internal static void ViewSessions(List<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Project Name");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.ProjectName,
                session.StartTime.ToString(),
                session.EndTime.ToString(),
                $"{session.Duration.TotalHours} hours {session.Duration.TotalMinutes % 60} minutes"
            );
        }

        AnsiConsole.Write(table);
    }
}
