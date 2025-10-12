using CodingTracker.Models;
using CodingTracker.Repository.CodingSessions;
using CodingTracker.Utils;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Services.CodingSessions;

public class CodingService : ICodingService
{
    private readonly ICodingRepository _codingRepository;

    public CodingService(ICodingRepository codingRepository)
    {
        _codingRepository = codingRepository;
    }

    public void AddSession()
    {
        CodingSession session = new();

        session.Project.Name = AnsiConsole.Ask<string>("Enter the project name:");

        var dates = Helpers.GetDates();
        session.StartTime = dates[0];
        session.EndTime = dates[1];

        _codingRepository.AddSession(session);
    }

    public List<CodingSession> GetAllSessions()
    {
        var sessions = _codingRepository.GetAllSessions();

        return sessions;
    }

    public CodingSession GetSession()
    {
        var sessions = GetAllSessions();

        if (!sessions.Any())
            AnsiConsole.MarkupLine(
                "[Red]No coding sessions to display. Please add new session.[/]"
            );

        UserInterface.ViewAllSessions(sessions);

        var sessionId = Helpers.GetSessionById(sessions);

        return _codingRepository.GetSession(sessionId);
    }

    public void UpdateSession()
    {
        var sessionToUpdate = GetSession();

        var updateStartTime = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the coding start and end time?")
                .AddChoices("Yes", "No")
        );
        if (updateStartTime == "Yes")
        {
            var dates = Helpers.GetDates();
            sessionToUpdate.StartTime = dates[0];
            sessionToUpdate.EndTime = dates[1];
        }
        else
        {
            sessionToUpdate.StartTime = sessionToUpdate.StartTime;
            sessionToUpdate.EndTime = sessionToUpdate.EndTime;
        }

        var updateCodingProjectName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the project name? ")
                .AddChoices("Yes", "No")
        );
        if (updateCodingProjectName == "Yes")
            sessionToUpdate.Project.Name = AnsiConsole.Ask<string>("Enter the project name:");
        else
            sessionToUpdate.Project.Name = sessionToUpdate.Project.Name;

        _codingRepository.UpdateSession(sessionToUpdate);
    }

    public void DeleteSession()
    {
        var sessions = GetAllSessions();

        var sessionId = Helpers.GetSessionById(sessions);

        if (_codingRepository.GetAllSessions().Count > 0)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
        }

        _codingRepository.DeleteSession(sessionId);
    }
}
