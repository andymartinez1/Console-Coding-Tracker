using CodingTracker.DTOs.CodingSessions;
using CodingTracker.Enums;

namespace CodingTracker.Services;

public interface ISessionService : ICrudService<AddSessionRequest, UpdateSessionRequest, SessionResponse, int>
{
    public Category GetCategory();

    bool IsStopwatchRunning();

    TimeSpan Elapsed();

    DateTime StartTimer();

    DateTime StopTimer();

    public void ResetTimer();

    public Task<SessionResponse> AddStopWatchSessionAsync();
}