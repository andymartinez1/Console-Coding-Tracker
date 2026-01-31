using System.Globalization;
using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Services.CodingSessions;
using Spectre.Console;

namespace CodingTracker.Views;

public class UserInterface
{
    public static void ViewAllSessions(List<SessionResponse> sessions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Project Name");
        table.AddColumn("Category");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var session in sessions)
            table.AddRow(
                session.SessionId.ToString(),
                session.Project.Name,
                session.Category,
                session.StartTime.ToString(CultureInfo.InvariantCulture),
                session.EndTime.ToString(CultureInfo.InvariantCulture),
                $"{Math.Floor(session.Duration.TotalHours)} hours, {session.Duration.TotalMinutes % 60} minutes"
            );

        AnsiConsole.Write(table);
    }

    public static void ViewSessionDetails(SessionResponse session)
    {
        var panel = new Panel(
                $"Project Name: {session.Project.Name} \nStart Time: {session.StartTime:g} \nEndTime: {session.EndTime:g} \nDuration: {session.Duration:g} \nCategory: {session.Category} \n"
            )
            .Header($"Details for ID: {session.SessionId}")
            .BorderStyle(Style.Parse("aquamarine1"));

        panel.Padding = new Padding(2);
        panel.Expand();

        AnsiConsole.Write(panel);
    }

    public static void ViewAllProjects(List<ProjectResponse> projects)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("Languages Used");

        foreach (var project in projects)
        {
            var languages =
                project.ProgrammingLanguages != null
                    ? string.Join(", ", project.ProgrammingLanguages)
                    : string.Empty;

            table.AddRow(
                project.Id.ToString(),
                project.Name ?? string.Empty,
                project.Description ?? string.Empty,
                languages
            );
        }

        AnsiConsole.Write(table);
    }

    public static void ViewProjectDetails(ProjectResponse project)
    {
        var languages =
            project.ProgrammingLanguages != null
                ? string.Join(", ", project.ProgrammingLanguages)
                : string.Empty;

        var panel = new Panel(
                $"Project Name: {project.Name} \nDescription: {project.Description} \nLanguages Used: {languages}"
            )
            .Header($"Details for ID: {project.Id}")
            .BorderStyle(Style.Parse("aquamarine1"));

        panel.Padding = new Padding(2);
        panel.Expand();

        AnsiConsole.Write(panel);
    }

    public static void ViewStopWatchTimer(ISessionService stopWatch)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow][b]Press any key to stop the timed session[/][/]");

        stopWatch.StartTimer();
        var live = AnsiConsole.Live(BuildPanel(stopWatch.Elapsed()));
        live.Start(ctx =>
        {
            while (true)
            {
                ctx.UpdateTarget(BuildPanel(stopWatch.Elapsed()));
                ctx.Refresh();

                Thread.Sleep(250);

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    stopWatch.StopTimer();
                    break;
                }
            }
        });
        stopWatch.AddStopWatchSession();

        static Panel BuildPanel(TimeSpan elapsed)
        {
            var elapsedStr = $"{(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
            return new Panel(new Markup($"[b]{elapsedStr}[/]"))
                .Header("Elapsed Time:")
                .BorderStyle(Style.Parse("aquamarine1"))
                .Padding(new Padding(1))
                .Expand();
        }
    }
}