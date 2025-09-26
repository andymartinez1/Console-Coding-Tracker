using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Views;

public class UserInterface
{
    public static void ViewAllSessions(List<CodingSession> sessions)
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
                session.Project.Name,
                session.StartTime.ToString(),
                session.EndTime.ToString(),
                $"{Math.Floor(session.Duration.TotalHours)} hours {session.Duration.TotalMinutes % 60} minutes"
            );

        AnsiConsole.Write(table);
    }

    public static void ViewSessionDetails(CodingSession session)
    {
        var panel = new Panel(
            $"Project Name: {session.Project.Name} \nStart Time: {session.StartTime:g} \nEndTime: {session.EndTime:g} \nDuration: {session.Duration:g} \n"
        )
            .Header($"Details for ID: {session.Id}")
            .BorderStyle(Style.Parse("aquamarine1"));

        panel.Padding = new Padding(2);
        panel.Expand();

        AnsiConsole.Write(panel);
    }
}
