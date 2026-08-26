using CodingTracker.DTOs.Projects;

namespace CodingTracker.Services.Projects;

public interface IProjectsService : ICrudService<AddProjectRequest, UpdateProjectRequest, ProjectResponse, int>
{
}