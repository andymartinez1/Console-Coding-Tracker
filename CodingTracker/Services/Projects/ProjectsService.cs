using CodingTracker.Models;
using CodingTracker.Repository.Projects;
using Spectre.Console;

namespace CodingTracker.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public void AddProject()
    {
        Project project = new Project();

        project.Name = AnsiConsole.Ask<string>("Enter the project name:");
        project.Description = AnsiConsole.Ask<string>("Enter the project description:");
    }

    public List<Project> GetAllProjects()
    {
        throw new NotImplementedException();
    }

    public Project GetProject()
    {
        throw new NotImplementedException();
    }

    public void UpdateProject()
    {
        throw new NotImplementedException();
    }

    public void DeleteProject()
    {
        throw new NotImplementedException();
    }
}
