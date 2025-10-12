namespace CodingTracker.Services.StopWatch;

public class StopWatchService : IStopWatchService
{
    private DateTime? _endTime;
    private DateTime? _startTime;

    public bool IsRunning { get; private set; }

    public void StartTimer()
    {
        _startTime = DateTime.Now;
        _endTime = null;
        IsRunning = true;
    }

    public void StopTimer()
    {
        if (IsRunning)
        {
            _endTime = DateTime.Now;
            IsRunning = false;
        }
    }

    public void ResetTimer()
    {
        _startTime = null;
        _endTime = null;
        IsRunning = false;
    }

    public TimeSpan GetElapsedTime()
    {
        if (IsRunning && _startTime.HasValue)
            return DateTime.Now - _startTime.Value;
        if (_startTime.HasValue && _endTime.HasValue)
            return _endTime.Value - _startTime.Value;
        return TimeSpan.Zero;
    }
}
