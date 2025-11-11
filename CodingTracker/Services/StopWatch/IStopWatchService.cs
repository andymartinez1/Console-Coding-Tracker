using CodingTracker.Models;

namespace CodingTracker.Services.StopWatch;

public interface IStopWatchService
{
    bool IsRunning { get; }

    TimeSpan Elapsed { get; }

    CodingSession? ViewSession { get; }
    void StartTimer();

    CodingSession? StopTimer();
}
