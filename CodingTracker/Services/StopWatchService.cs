namespace CodingTracker.Services;

public class StopWatchService : IStopWatchService
{
    private DateTime? _startTime;
    private DateTime? _endTime;
    private bool _isRunning;

    public void StartTimer()
    {
        _startTime = DateTime.Now;
        _endTime = null;
        _isRunning = true;
    }

    public void StopTimer()
    {
        if (_isRunning)
        {
            _endTime = DateTime.Now;
            _isRunning = false;
        }
    }

    public void ResetTimer()
    {
        _startTime = null;
        _endTime = null;
        _isRunning = false;
    }

    public TimeSpan GetElapsedTime()
    {
        if (_isRunning && _startTime.HasValue)
            return DateTime.Now - _startTime.Value;
        if (_startTime.HasValue && _endTime.HasValue)
            return _endTime.Value - _startTime.Value;
        return TimeSpan.Zero;
    }

    public bool IsRunning => _isRunning;
}
