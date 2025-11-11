using CodingTracker.Models;

namespace CodingTracker.Services.Projects;

public interface IProjectsService
{
    public void AddProject();

    public List<Project> GetAllProjects();

    public Project GetProject();

    public void ViewProjectById();

    public void UpdateProject();

    public void DeleteProject();
}
