using System.Globalization;
using CodingTracker.Models;
using CodingTracker.Utils;
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
                session.StartTime.ToString(CultureInfo.InvariantCulture),
                session.EndTime.ToString(CultureInfo.InvariantCulture),
                $"{Math.Floor(session.Duration.TotalHours)} hours, {session.Duration.TotalMinutes % 60} minutes"
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

    public static void ViewAllLanguages(List<ProgrammingLanguage> languages)
    {
        if (!Validation.IsListEmpty(languages))
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Language");
            table.AddColumn("Version");

            foreach (var language in languages)
                table.AddRow(
                    language.Id.ToString(),
                    language.Language,
                    language.Version.ToString()
                );

            AnsiConsole.Write(table);
        }
    }

    public static void ViewLanguageDetails(ProgrammingLanguage language)
    {
        var panel = new Panel(
            $"Programming Language: {language.Language} \nVersion: {language.Version} \n"
        )
            .Header($"Details for ID: {language.Id}")
            .BorderStyle(Style.Parse("aquamarine1"));

        panel.Padding = new Padding(2);
        panel.Expand();

        AnsiConsole.Write(panel);
    }

    public static void ViewAllProjects(List<Project> projects)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("Category");

        foreach (var project in projects)
            table.AddRow(
                project.Id.ToString(),
                project.Name,
                project.Description,
                project.Category.ToString()
            );

        AnsiConsole.Write(table);
    }

    public static void ViewProjectDetails(Project project)
    {
        var panel = new Panel(
            $"Project Name: {project.Name} \nDescription: {project.Description} \nCategory: {project.Category} \n"
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
