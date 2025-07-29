using CodingTracker.Models;

namespace CodingTracker.Services;

public interface ICodingService
{
    public List<CodingSession> GetAllSessions();

    public CodingSession GetSession(int id);

    public void AddSession();

    public void UpdateSession(CodingSession session);

    public void DeleteSession(int id);
}
