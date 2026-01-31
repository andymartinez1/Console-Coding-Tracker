using CodingTracker.Models;

namespace CodingTracker.Repository.CodingSessions;

public interface ISessionRepository
{
    public void AddSession(CodingSession session);

    public List<CodingSession> GetAllSessions();

    public CodingSession GetSession(int id);

    public void UpdateSession(CodingSession session);

    public void DeleteSession(int id);
}