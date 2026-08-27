using CodingTracker.Data;
using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Repository;

public class SessionRepository : ISessionRepository
{
    private readonly CodingDbContext _codingDbContext;

    public SessionRepository(CodingDbContext codingDbContext)
    {
        _codingDbContext = codingDbContext;
    }

    public async Task AddAsync(CodingSession entity, CancellationToken cancellationToken = default)
    {
        await _codingDbContext.CodingSessions.AddAsync(entity, cancellationToken);

        await _codingDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CodingSession>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _codingDbContext.CodingSessions
            .Include(cs => cs.Project)
            .ToListAsync(cancellationToken);
    }

    public async Task<CodingSession?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _codingDbContext.CodingSessions.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(CodingSession entity, CancellationToken cancellationToken = default)
    {
        _codingDbContext.CodingSessions.Update(entity);

        await _codingDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var session = await GetByIdAsync(id, cancellationToken);

        if (session is null) return;

        _codingDbContext.CodingSessions.Remove(session);

        await _codingDbContext.SaveChangesAsync(cancellationToken);
    }
}