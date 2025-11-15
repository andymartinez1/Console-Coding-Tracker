using CodingTracker.Models;

namespace CodingTracker.Services.CodingSessions;

public interface ISessionService
{
    public void AddSession();

    public List<CodingSession> GetAllSessions();

    public CodingSession? GetSession();

    public void ViewSessionById();

    public void UpdateSession();

    public void DeleteSession();
}
