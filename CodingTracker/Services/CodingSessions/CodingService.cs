using CodingTracker.Models;
using CodingTracker.Repository;
using CodingTracker.Utils;
using Spectre.Console;

namespace CodingTracker.Services;

public class CodingService : ICodingService
{
    private readonly ICodingRepository _codingRepository;

    public CodingService(ICodingRepository codingRepository)
    {
        _codingRepository = codingRepository;
    }

    public List<CodingSession> GetAllSessions()
    {
        var sessions = _codingRepository.GetAllSessions();

        return sessions;
    }

    public CodingSession GetSession(int id)
    {
        if (_codingRepository.GetAllSessions().Count > 0)
        {
            var session = _codingRepository.GetSession(id);
            return session;
        }

        return null;
    }

    public void AddSession()
    {
        CodingSession session = new();

        session.Project.Name = AnsiConsole.Ask<string>("Enter the project name:");

        var dates = Helpers.GetDates();
        session.StartTime = dates[0];
        session.EndTime = dates[1];

        _codingRepository.InsertSession(session);
    }

    public void UpdateSession(CodingSession session)
    {
        if (_codingRepository.GetAllSessions().Count > 0)
        {
            var updateStartTime = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Would you like to update the coding start and end time?")
                    .AddChoices("Yes", "No")
            );
            if (updateStartTime == "Yes")
            {
                var dates = Helpers.GetDates();
                session.StartTime = dates[0];
                session.EndTime = dates[1];
            }
            else
            {
                session.StartTime = session.StartTime;
                session.EndTime = session.EndTime;
            }

            var updateCodingProjectName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Would you like to update the project name? ")
                    .AddChoices("Yes", "No")
            );
            if (updateCodingProjectName == "Yes")
                session.Project.Name = AnsiConsole.Ask<string>("Enter the project name:");
            else
                session.Project.Name = session.Project.Name;

            _codingRepository.UpdateSession(session);
        }
    }

    public void DeleteSession(int id)
    {
        Helpers.GetSessionById();

        if (_codingRepository.GetAllSessions().Count > 0)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
        }

        _codingRepository.DeleteSession(id);
    }
}
