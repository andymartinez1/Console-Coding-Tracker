using CodingTracker.Models;

namespace CodingTracker.Services;

public interface ICodingService
{
    public List<CodingSession> GetAllSessions();

    public CodingSession GetSession();

    public void AddSession();

    public void UpdateSession();

    public void DeleteSession();
}
