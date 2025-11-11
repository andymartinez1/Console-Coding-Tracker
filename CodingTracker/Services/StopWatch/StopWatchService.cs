using System.Diagnostics;
using CodingTracker.Models;

namespace CodingTracker.Services.StopWatch;

public class StopWatchService : IStopWatchService
{
    private readonly object _lock = new();
    private readonly Stopwatch _sw = new();
    private CodingSession? _currentSession;

    public bool IsRunning
    {
        get
        {
            lock (_lock)
            {
                return _sw.IsRunning;
            }
        }
    }

    public TimeSpan Elapsed
    {
        get
        {
            lock (_lock)
            {
                if (_sw.IsRunning)
                    return _sw.Elapsed;
                if (_currentSession?.StartTime != null && _currentSession?.EndTime != null)
                    return _currentSession.EndTime - _currentSession.StartTime;
                if (_currentSession?.StartTime != null)
                    return DateTime.Now - _currentSession.StartTime;
                return TimeSpan.Zero;
            }
        }
    }

    public CodingSession? ViewSession
    {
        get
        {
            lock (_lock)
            {
                return _currentSession;
            }
        }
    }

    public void StartTimer()
    {
        var session = new CodingSession();

        // If already running, ignore or optionally throw
        if (_sw.IsRunning)
            return;

        session.StartTime = DateTime.Now;
        _currentSession = session;

        _sw.Reset();
        _sw.Start();
    }

    public CodingSession? StopTimer()
    {
        lock (_lock)
        {
            if (_currentSession == null)
                return null;
            if (_sw.IsRunning)
                _sw.Stop();

            // Use wall-clock time for the session end time so saved timestamps match displayed time
            _currentSession.EndTime = DateTime.Now;

            var finished = _currentSession;
            _currentSession = null;
            return finished;
        }
    }

    public void ResetTimer()
    {
        lock (_lock)
        {
            _sw.Reset();
            _currentSession = null;
        }
    }
}
