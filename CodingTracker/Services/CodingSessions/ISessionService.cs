using CodingTracker.DTOs.CodingSessions;
using CodingTracker.Enums;

namespace CodingTracker.Services.CodingSessions;

public interface ISessionService
{
    public void AddSession(AddSessionRequest sessionRequest);

    public List<SessionResponse> GetAllSessions();

    public SessionResponse? GetSession();

    public void ViewSessionById();

    public void UpdateSession();

    public bool DeleteSession();

    public Category GetCategory();

    bool IsStopwatchRunning();

    TimeSpan Elapsed();

    DateTime StartTimer();

    DateTime StopTimer();

    public void ResetTimer();

    public void AddStopWatchSession();
}