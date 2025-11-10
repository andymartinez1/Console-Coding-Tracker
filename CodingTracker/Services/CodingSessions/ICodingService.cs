using CodingTracker.Models;

namespace CodingTracker.Services.CodingSessions;

public interface ICodingService
{
    public void AddSession();

    public List<CodingSession> GetAllSessions();

    public CodingSession GetSession();

    void ViewSessionById();

    public void UpdateSession();

    public void DeleteSession();
}
