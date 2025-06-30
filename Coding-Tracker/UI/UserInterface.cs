using Coding_Tracker.Models;
using Spectre.Console;

namespace Coding_Tracker.UI;

public class UserInterface
{
    internal static void ViewSessions(List<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Project Name");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var session in sessions)
            table.AddRow(
                session.Id.ToString(),
                session.ProjectName,
                session.StartTime.ToString(),
                session.EndTime.ToString(),
                $"{Math.Floor(session.Duration.TotalHours)} hours {session.Duration.TotalMinutes % 60} minutes"
            );

        AnsiConsole.Write(table);
    }
}
