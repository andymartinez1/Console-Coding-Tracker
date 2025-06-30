using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Coding_Tracker.Services;
using Coding_Tracker.UI;
using Spectre.Console;

namespace Coding_Tracker.Controllers;

internal class CodingController
{
    internal static DataConnection data = new();

    internal static void AddSession()
    {
        CodingSession session = new();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        if (session.ProjectName == null)
            Menu.MainMenu();

        var Dates = GetDates();
        session.StartTime = Dates[0];
        session.EndTime = Dates[1];

        var data = new DataConnection();
        data.InsertSession(session);
    }

    internal static void UpdateSession(int id)
    {
        var sessions = data.GetAllSessions();
        Validation.ValidateSessionsList(sessions);

        var sessionId = GetSessionId();

        var session = sessions.Where(s => s.Id == sessionId).FirstOrDefault();
        var dates = GetDates();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        session.StartTime = dates[0];
        session.EndTime = dates[1];
        var sessionDuration = session.Duration;

        data.UpdateSession(session);
    }

    internal static void DeleteSession(int id)
    {
        var sessions = data.GetAllSessions();
        Validation.ValidateSessionsList(sessions);

        var sessionId = GetSessionId();

        AnsiConsole.WriteLine("Session deleted successfully!");
        data.DeleteSession(sessionId);
    }

    internal static DateTime[] GetDates()
    {
        return Validation.ValidateDateTime();
    }

    internal static void SeedSessions(int count)
    {
        var random = new Random();
        var currentDate = DateTime.Now.Date;

        var sessions = new List<CodingSession>();

        for (var i = 0; i < count; i++)
        {
            var startTime = currentDate.AddHours(random.Next(0, 12)).AddMinutes(random.Next(0, 60));
            var endTime = startTime.AddHours(random.Next(1, 12)).AddMinutes(random.Next(0, 60));

            var session = new CodingSession
            {
                ProjectName = $"Project {i + 1}",
                StartTime = startTime,
                EndTime = endTime,
            };

            var sessionDuration = session.Duration;
            sessions.Add(session);
            currentDate = currentDate.AddDays(1);
        }

        var data = new DataConnection();
        data.InsertSeedSessions(sessions);
    }

    public static int GetSessionId()
    {
        AnsiConsole.Clear();

        var sessions = data.GetAllSessions();
        UserInterface.ViewSessions(sessions);

        var sessionArray = sessions.Select(s => s.Id).ToArray();

        if (sessionArray.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]No sessions found![/]");
            AnsiConsole.MarkupLine("Please add a session first.");
            Menu.MainMenu();
        }
        else
        {
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<int>().Title("Select the session:").AddChoices(sessionArray)
            );
            return option;
        }

        return 0; // Fallback return in case of no sessions
    }
}
