using CodingTracker.Data;
using CodingTracker.Models;

namespace CodingTracker.Repository.CodingSessions;

public class CodingRepository : ICodingRepository
{
    private readonly CodingDbContext _codingDbContext;

    public CodingRepository(CodingDbContext codingDbContext)
    {
        _codingDbContext = codingDbContext;
    }

    public void AddSession(CodingSession session)
    {
        _codingDbContext.CodingSessions.Add(session);

        _codingDbContext.SaveChanges();
    }

    public List<CodingSession> GetAllSessions()
    {
        return _codingDbContext.CodingSessions.ToList();
    }

    public CodingSession GetSession(int id)
    {
        return _codingDbContext.CodingSessions.Find(id);
    }

    public void UpdateSession(CodingSession session)
    {
        _codingDbContext.CodingSessions.Update(session);

        _codingDbContext.SaveChanges();
    }

    public void DeleteSession(int id)
    {
        var session = GetSession(id);

        _codingDbContext.CodingSessions.Remove(session);

        _codingDbContext.SaveChanges();
    }
}
