using CodingTracker.DTOs.Projects;

namespace CodingTracker.Services;

public interface IProjectsService : ICrudService<AddProjectRequest, UpdateProjectRequest, ProjectResponse, int>
{
}