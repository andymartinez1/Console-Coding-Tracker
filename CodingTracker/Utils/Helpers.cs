using System.Globalization;
using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Enums;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Utils;

public static class Helpers
{
    public static SessionResponse SelectSessionById(List<SessionResponse> sessions)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllSessions(sessions);

        if (!sessions.Any())
            return null;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<SessionResponse>().Title("Select the session:").AddChoices(sessions)
                .UseConverter(s => $"{s.SessionId}: {s.Project.Name} - {s.Category} - {s.Duration}")
        );
        return option;
    }

    public static ProjectResponse SelectProjectById(List<ProjectResponse> projects)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllProjects(projects);

        if (!projects.Any())
            return null;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<ProjectResponse>()
                .Title("Select the project:")
                .AddChoices(projects)
                .UseConverter(p => $"{p.Id} - {p.Name}")
        );
        return option;
    }

    public static Category SelectCategory(List<Category> categories)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllCategories(categories);

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<Category>()
                .Title("Select the category:")
                .AddChoices(categories)
                .UseConverter(c => c.ToString())
        );
        return option;
    }

    public static DateTime[] GetDates()
    {
        var startDateInput = AnsiConsole.Ask<string>(
            "Enter Coding Start Time (yyyy-MM-dd HH:mm): "
        );

        while (!Validation.IsValidDate(startDateInput, "yyyy-MM-dd HH:mm"))
            startDateInput = AnsiConsole.Ask<string>(
                "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
            );

        var endDateInput = AnsiConsole.Ask<string>("Enter Coding End Time (yyyy-MM-dd HH:mm): ");

        while (!Validation.IsValidDate(endDateInput, "yyyy-MM-dd HH:mm"))
            endDateInput = AnsiConsole.Ask<string>(
                "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
            );

        while (!Validation.IsStartDateBeforeEndDate(startDateInput, endDateInput))
        {
            AnsiConsole.MarkupLine(
                "\n[red]Start date must be before end date. Please try again:[/]"
            );
            startDateInput = AnsiConsole.Ask<string>(
                "Enter Coding Start Time (yyyy-MM-dd HH:mm): "
            );

            while (!Validation.IsValidDate(startDateInput, "yyyy-MM-dd HH:mm"))
                startDateInput = AnsiConsole.Ask<string>(
                    "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
                );

            endDateInput = AnsiConsole.Ask<string>("Enter Coding End Time (yyyy-MM-dd HH:mm): ");

            while (!Validation.IsValidDate(endDateInput, "yyyy-MM-dd HH:mm"))
                endDateInput = AnsiConsole.Ask<string>(
                    "\n[red]Invalid date. Format: yyyy-MM-dd HH:mm. Please try again:[/]\n"
                );
        }

        var startDate = DateTime.ParseExact(
            startDateInput,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );
        var endDate = DateTime.ParseExact(
            endDateInput,
            "yyyy-MM-dd HH:mm",
            CultureInfo.InvariantCulture
        );

        return [startDate, endDate];
    }
}