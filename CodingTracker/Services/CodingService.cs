using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Coding_Tracker.Utils;
using Spectre.Console;

namespace Coding_Tracker.Services;

public class CodingService : ICodingService
{
    private readonly DataConnection _dataConnection;

    public CodingService(DataConnection dataConnection)
    {
        _dataConnection = dataConnection;
    }

    public List<CodingSession> GetAllSessions()
    {
        var sessions = _dataConnection.GetAllSessions();

        return sessions;
    }

    public CodingSession GetSessionById(int id)
    {
        throw new NotImplementedException();
    }

    public void AddSession()
    {
        CodingSession session = new();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        if (session.ProjectName == null)
            return;

        var dates = Helpers.GetDates();
        session.StartTime = dates[0];
        session.EndTime = dates[1];

        _dataConnection.InsertSession(session);
    }

    public void UpdateSession(int id)
    {
        var sessions = _dataConnection.GetAllSessions();
        Validation.ValidateSessionsList(sessions);

        var sessionId = Helpers.GetSessionId(sessions);

        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
        var dates = Helpers.GetDates();

        session.ProjectName = AnsiConsole.Ask<string>("Enter the project name:");
        session.StartTime = dates[0];
        session.EndTime = dates[1];

        _dataConnection.UpdateSession(session);
    }

    public void DeleteSession(int id)
    {
        var sessions = _dataConnection.GetAllSessions();
        Validation.ValidateSessionsList(sessions);

        var sessionId = Helpers.GetSessionId(sessions);

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");

        _dataConnection.DeleteSession(sessionId);
    }
}
