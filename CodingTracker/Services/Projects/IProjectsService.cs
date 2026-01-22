using CodingTracker.DTOs.Projects;

namespace CodingTracker.Services.Projects;

public interface IProjectsService
{
    public void AddProject(AddProjectRequest projectRequest);

    public List<ProjectResponse> GetAllProjects();

    public ProjectResponse? GetProject();

    public void ViewProjectById();

    public void UpdateProject();

    public bool DeleteProject();
}
