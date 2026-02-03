using System.Globalization;
using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Services.CodingSessions;
using CodingTracker.Utils;
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
                Helpers.FormatDuration(session.Duration)
            );

        AnsiConsole.Write(table);
    }

    public static void ViewSessionDetails(SessionResponse session)
    {
        var panel = new Panel(
                $"Project Name: {session.Project.Name} \n" +
                $"Start Time: {session.StartTime:g} \n" +
                $"EndTime: {session.EndTime:g} \n" +
                $"Duration: {Helpers.FormatDuration(session.Duration)} \n" +
                $"Category: {session.Category} \n"
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

        if (stopWatch.IsStopwatchRunning())
        {
            var alreadyRunning = new Panel(
                    "[yellow][b]RUNNING[/][/]\n\n" +
                    "[grey]A timed session is already active.[/]\n" +
                    "[grey]Use the Timer menu and choose[/] [white][b]Stop timer[/][/] [grey]to finish and save it.[/]\n\n"
                )
                .Header("[yellow]Timer Status[/]")
                .BorderStyle(Style.Parse("yellow"));

            alreadyRunning.Padding = new Padding(1, 1, 1, 1);
            alreadyRunning.Expand();

            AnsiConsole.Write(alreadyRunning);
            return;
        }

        var startTime = stopWatch.StartTimer();

        var runningPanel = new Panel(
                "[green][b]RUNNING[/][/]\n\n" +
                $"[grey]Started:[/] [aqua]{startTime:yyyy-MM-dd HH:mm:ss}[/]\n" +
                "[grey]Next step:[/] Go back to the Timer menu and choose " +
                "[white][b]Stop timed session[/][/] [grey]to stop and save.[/]"
            )
            .Header("[green]Timer Status[/]")
            .BorderStyle(Style.Parse("green"));

        runningPanel.Padding = new Padding(1, 1, 1, 1);
        runningPanel.Expand();

        AnsiConsole.Write(runningPanel);
    }
}