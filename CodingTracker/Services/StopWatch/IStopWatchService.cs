namespace CodingTracker.Services.StopWatch;

public interface IStopWatchService
{
    public void StartTimer();

    public void StopTimer();

    public void ResetTimer();

    public TimeSpan GetElapsedTime();
}
