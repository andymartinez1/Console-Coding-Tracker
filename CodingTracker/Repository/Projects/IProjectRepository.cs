using CodingTracker.Models;

namespace CodingTracker.Repository.Projects;

public interface IProjectRepository
{
    public void InsertProject(Project project);

    public List<Project> GetAllProjects();

    public Project GetProject(int id);

    public void UpdateProject(Project project);

    public void DeleteProject(int id);
}
