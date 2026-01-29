using System.Globalization;
using CodingTracker.DTOs.Projects;
using CodingTracker.Enums;
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

    public static void ViewSessionDetails(CodingSession session)
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

    public static void ViewAllCategories(List<Category> categories)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Category");

        foreach (var category in categories) { }
    }

    public static void ViewStopWatchTimer() { }
}
