using System.Globalization;
using CodingTracker.Models;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Utils;

public static class Helpers
{
    public static int GetSessionById(List<CodingSession> sessions)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllSessions(sessions);

        var sessionArray = sessions.Select(s => s.Id).ToArray();

        if (Validation.IsListEmpty(sessions))
            return 0;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<int>().Title("Select the session:").AddChoices(sessionArray)
        );
        return option;
    }

    public static int GetLanguageById(List<ProgrammingLanguage> languages)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllLanguages(languages);

        var sessionArray = languages.Select(s => s.Id).ToArray();

        if (Validation.IsListEmpty(languages))
            return 0;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<int>().Title("Select the language:").AddChoices(sessionArray)
        );
        return option;
    }

    public static int GetProjectById(List<Project> projects)
    {
        AnsiConsole.Clear();

        UserInterface.ViewAllProjects(projects);

        var sessionArray = projects.Select(s => s.Id).ToArray();

        if (Validation.IsListEmpty(projects))
            return 0;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<int>().Title("Select the project:").AddChoices(sessionArray)
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
