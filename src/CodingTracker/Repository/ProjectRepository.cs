using CodingTracker.Data;
using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly CodingDbContext _dbContext;

    public ProjectRepository(CodingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Project entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Projects.AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Projects.ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Projects.FindAsync([id], cancellationToken);
    }

    public async Task UpdateAsync(Project entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Projects.Update(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await GetByIdAsync(id, cancellationToken);

        if (project is null) return;

        _dbContext.Projects.Remove(project);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}