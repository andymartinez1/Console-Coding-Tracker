using Coding_Tracker.Models;

namespace Coding_Tracker.Services;

public interface ICodingService
{
    public List<CodingSession> GetAllSessions();

    public CodingSession GetSessionById(int id);

    public void AddSession();

    public void UpdateSession(int id);

    public void DeleteSession(int id);
}
