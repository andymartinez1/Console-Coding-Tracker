using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Coding_Tracker.UI;
using Spectre.Console;
using System.Globalization;

namespace Coding_Tracker.Controllers;

internal class SessionController
{

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
                $"{Math.Floor(session.Duration.TotalHours)} hours {session.Duration.TotalMinutes % 60} minutes"
            );
        }

        AnsiConsole.Write(table);
    }

    internal static void UpdateSession(int id)
    {
        var data = new DataConnection();
        var sessions = data.GetAllSessions();
        ViewSessions(sessions);

        var sessionId = AnsiConsole.Ask<int>("Enter the ID of the session you want to update:");
        if (sessionId <= 0 || sessionId > sessions.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid session ID. Please try again.[/]");
            return;
        }

        var session = sessions.Where(s => s.Id == sessionId).FirstOrDefault();
        var dates = GetDates();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        session.StartTime = dates[0];
        session.EndTime = dates[1];
        session.Duration = session.EndTime - session.StartTime;

        data.UpdateSession(session);
    }

    internal static void DeleteSession(int id)
    {
        var data = new DataConnection();
        var sessions = data.GetAllSessions();
        ViewSessions(sessions);

        var sessionId = AnsiConsole.Ask<int>("Enter the ID of the session you want to delete:");
        if (sessionId <= 0 || sessionId > sessions.Count)
        {
            AnsiConsole.MarkupLine("[red]Invalid session ID. Please try again.[/]");
            return;
        }

        if (!AnsiConsole.Confirm("Are you sure?")) return;

        AnsiConsole.WriteLine("Session deleted successfully!");
        data.DeleteSession(sessionId);
    }

    internal static DateTime[] GetDates()
    {
        DateTime startDate;
        DateTime endDate;

        var startDateInput = AnsiConsole.Ask<string>("Enter the start date (MM-dd-yyyy hh:mm ):");
        if (startDateInput == null) Menu.MainMenu();

        while (!DateTime.TryParseExact(startDateInput, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
        {
            startDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: mm-dd-yyyy HH:mm. PLease try again\n\n");
        }

        var endDateInput = AnsiConsole.Ask<string>("Enter the end date (MM-dd-yyyy hh:mm):");
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

    internal static void SeedSessions(int count)
    {
        Random random = new Random();
        DateTime currentDate = DateTime.Now.Date;

        List<CodingSession> sessions = new List<CodingSession>();

        for (int i = 0; i < count; i++)
        {
            DateTime startTime = currentDate.AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60));
            DateTime endTime = startTime.AddHours(random.Next(1, 12)).AddMinutes(random.Next(0, 60));

            CodingSession session = new CodingSession
            {
                ProjectName = $"Project {i + 1}",
                StartTime = startTime,
                EndTime = endTime
            };

            session.Duration = session.EndTime - session.StartTime;
            sessions.Add(session);
            currentDate = currentDate.AddDays(1);
        }

        var data = new DataConnection();
        data.InsertSeedSessions(sessions);
    }
}
