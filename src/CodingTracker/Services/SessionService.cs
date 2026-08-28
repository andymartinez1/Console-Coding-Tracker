using System.Diagnostics;
using CodingTracker.DTOs.CodingSessions;
using CodingTracker.DTOs.Projects;
using CodingTracker.Enums;
using CodingTracker.Repository;
using CodingTracker.Utils;
using CodingTracker.Views;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CodingTracker.Services;

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

    public async Task<SessionResponse> AddAsync(AddSessionRequest? sessionRequest)
    {
        sessionRequest ??= new AddSessionRequest();

        var projects = await _projectsService.GetAllAsync();
        sessionRequest.ProjectId = Helpers.SelectProjectById(projects).ToProjectEntity().ProjectId;
        sessionRequest.Category = GetCategory();

        var dates = Helpers.GetDates();
        sessionRequest.StartTime = dates[0];
        sessionRequest.EndTime = dates[1];

        var session = sessionRequest.ToSessionEntity();
        await _sessionRepository.AddAsync(session);
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session added successfully![/]");
        _logger.LogInformation("Session with ID: {SessionId} added.", session.SessionId);

        return session.ToSessionResponse();
    }

    public async Task<List<SessionResponse>> GetAllAsync()
    {
        var sessions = await _sessionRepository.GetAllAsync();

        if (!sessions.Any())
            AnsiConsole.MarkupLine("[Red]No coding sessions to display. Please add a new session.[/]");

        var sessionResponses = sessions.Select(s => s.ToSessionResponse()).ToList();

        UserInterface.ViewAllSessions(sessionResponses);
        return sessionResponses;
    }

    public async Task<SessionResponse?> GetByIdAsync(int id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);

        if (session is null)
            return null;

        var sessionResponse = session.ToSessionResponse();
        _logger.LogInformation("Session with ID: {SessionId} retrieved.", id);

        return sessionResponse;
    }

    public async Task<SessionResponse?> UpdateAsync(UpdateSessionRequest? request)
    {
        var sessions = await GetAllAsync();
        var sessionResponse = Helpers.SelectSessionById(sessions);
        var sessionToUpdate = await _sessionRepository.GetByIdAsync(sessionResponse.SessionId);

        if (sessionToUpdate is null)
            return null;

        var updateChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Session Times", "Category")
        );

        switch (updateChoice)
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

        await _sessionRepository.UpdateAsync(sessionToUpdate);
        _logger.LogInformation("Session with ID: {SessionId} updated.", sessionToUpdate.SessionId);
        AnsiConsole.Clear();
        await GetAllAsync();

        return sessionToUpdate.ToSessionResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sessions = await GetAllAsync();

        if (!sessions.Any())
            return false;

        var sessionResponse = Helpers.SelectSessionById(sessions);

        await _sessionRepository.DeleteAsync(sessionResponse.SessionId);
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
        _logger.LogInformation("Session with ID: {SessionId} deleted.", sessionResponse.SessionId);

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

    public async Task<SessionResponse> AddStopWatchSessionAsync()
    {
        if (_timerStartTime == null || _timerEndTime == null)
        {
            AnsiConsole.MarkupLine("[yellow]No completed timed session to save. Start a timer, then stop it.[/]");
            throw new InvalidOperationException("No completed timed session to save.");
        }

        var projects = await _projectsService.GetAllAsync();

        var timedSession = new AddSessionRequest
        {
            Category = GetCategory(),
            ProjectId = Helpers.SelectProjectById(projects).ToProjectEntity().ProjectId,
            StartTime = _timerStartTime.Value,
            EndTime = _timerEndTime.Value
        };

        var session = timedSession.ToSessionEntity();
        await _sessionRepository.AddAsync(session);

        _timerStartTime = null;
        _timerEndTime = null;

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Session added successfully![/]");
        _logger.LogInformation("Session with ID: {SessionId} added.", session.SessionId);

        return session.ToSessionResponse();
    }
}