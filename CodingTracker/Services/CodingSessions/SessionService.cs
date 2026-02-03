using System.Diagnostics;
using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Enums;
using CodingTracker.Repository.CodingSessions;
using CodingTracker.Services.Projects;
using CodingTracker.Utils;
using CodingTracker.Views;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CodingTracker.Services.CodingSessions;

public class SessionService : ISessionService
{
    private readonly ILogger<SessionService> _logger;
    private readonly IProjectsService _projectsService;
    private readonly ISessionRepository _sessionRepository;
    private readonly Stopwatch _stopwatch = new();
    private DateTime? _timerEndTime;

    private DateTime? _timerStartTime;

    public SessionService(ISessionRepository sessionRepository, ILogger<SessionService> logger,
        IProjectsService projectsService)
    {
        _sessionRepository = sessionRepository;
        _logger = logger;
        _projectsService = projectsService;
    }

    public void AddSession(AddSessionRequest sessionRequest)
    {
        var projects = _projectsService.GetAllProjects();
        sessionRequest.ProjectId = Helpers.SelectProjectById(projects).ToProjectEntity().ProjectId;

        sessionRequest.Category = GetCategory();

        var dates = Helpers.GetDates();
        sessionRequest.StartTime = dates[0];
        sessionRequest.EndTime = dates[1];

        var session = sessionRequest.ToSessionEntity();
        _sessionRepository.AddSession(session);
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session added successfully![/]");
        _logger.LogInformation("Session with ID: {sessionId} added.", session.SessionId);
    }

    public List<SessionResponse> GetAllSessions()
    {
        var sessions = _sessionRepository.GetAllSessions();

        if (!sessions.Any())
            AnsiConsole.MarkupLine(
                "[Red]No coding sessions to display. Please add a new session.[/]"
            );

        var sessionResponses = sessions.Select(s => s.ToSessionResponse()).ToList();

        UserInterface.ViewAllSessions(sessionResponses);
        return sessionResponses;
    }

    public SessionResponse? GetSession()
    {
        var sessions = GetAllSessions();

        if (!sessions.Any())
            return null;

        UserInterface.ViewAllSessions(sessions);
        var session = Helpers.SelectSessionById(sessions);

        var sessionResponse = _sessionRepository.GetSession(session.SessionId).ToSessionResponse();
        _logger.LogInformation("Session with ID: {projectId} retrieved.", session.SessionId);

        return sessionResponse;
    }

    public void ViewSessionById()
    {
        var session = GetSession();

        if (session == null)
            return;

        UserInterface.ViewSessionDetails(session);
    }

    public void UpdateSession()
    {
        var sessions = GetAllSessions();
        var sessionResponse = Helpers.SelectSessionById(sessions);
        var sessionToUpdate = _sessionRepository.GetSession(sessionResponse.SessionId);

        var updateSession = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Session Times", "Category")
        );

        switch (updateSession)
        {
            case "Session Times":
                var dates = Helpers.GetDates();
                sessionToUpdate.StartTime = dates[0];
                sessionToUpdate.EndTime = dates[1];
                break;
            case "Category":
                sessionToUpdate.Category = GetCategory().ToString();
                break;
        }

        _sessionRepository.UpdateSession(sessionToUpdate);
        _logger.LogInformation("Session with ID: {sessionId} updated.", sessionToUpdate.SessionId);
        AnsiConsole.Clear();
        GetAllSessions();
    }

    public bool DeleteSession()
    {
        var sessions = GetAllSessions();

        if (!sessions.Any())
            return false;

        var sessionResponse = Helpers.SelectSessionById(sessions);

        _sessionRepository.DeleteSession(sessionResponse.SessionId);
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
        _logger.LogInformation("Session with ID: {sessionId} deleted.", sessionResponse.SessionId);
        return true;
    }

    public Category GetCategory()
    {
        var categories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();

        return Helpers.SelectCategory(categories);
    }

    public bool IsStopwatchRunning()
    {
        return _stopwatch.IsRunning;
    }

    public TimeSpan Elapsed()
    {
        return _stopwatch.Elapsed;
    }

    public DateTime StartTimer()
    {
        var startTime = DateTime.Now;

        _timerStartTime = startTime;
        _timerEndTime = null;

        _stopwatch.Restart();

        return startTime;
    }

    public DateTime StopTimer()
    {
        _stopwatch.Stop();

        var endTime = DateTime.Now;

        _timerEndTime = endTime;

        return endTime;
    }

    public void ResetTimer()
    {
        _stopwatch.Reset();
    }

    public void AddStopWatchSession()
    {
        if (_timerStartTime == null || _timerEndTime == null)
        {
            AnsiConsole.MarkupLine("[yellow]No completed timed session to save. Start a timer, then stop it.[/]");
            return;
        }

        var projects = _projectsService.GetAllProjects();

        var timedSession = new AddSessionRequest
        {
            Category = GetCategory(),
            ProjectId = Helpers.SelectProjectById(projects).ToProjectEntity().ProjectId,
            StartTime = _timerStartTime.Value,
            EndTime = _timerEndTime.Value
        };

        var session = timedSession.ToSessionEntity();
        _sessionRepository.AddSession(session);

        _timerStartTime = null;
        _timerEndTime = null;

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session added successfully![/]");
        _logger.LogInformation("Session with ID: {sessionId} added.", session.SessionId);
    }
}