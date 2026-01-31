using CodingTracker.Data;
using CodingTracker.Models;

namespace CodingTracker.Repository.Projects;

public class ProjectRepository : IProjectRepository
{
    private readonly CodingDbContext _dbContext;

    public ProjectRepository(CodingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddProject(Project project)
    {
        _dbContext.Projects.Add(project);

        _dbContext.SaveChanges();
    }

    public List<Project> GetAllProjects()
    {
        var projects = _dbContext.Projects.ToList();

        return projects;
    }

    public Project GetProject(int id)
    {
        return _dbContext.Projects.Find(id);
    }

    public void UpdateProject(Project project)
    {
        _dbContext.Projects.Update(project);

        _dbContext.SaveChanges();
    }

    public void DeleteProject(int id)
    {
        var project = GetProject(id);

        _dbContext.Projects.Remove(project);

        _dbContext.SaveChanges();
    }
}