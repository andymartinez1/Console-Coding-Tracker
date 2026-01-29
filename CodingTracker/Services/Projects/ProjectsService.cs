using CodingTracker.DTOs.Projects;
using CodingTracker.Repository.Projects;
using CodingTracker.Utils;
using CodingTracker.Views;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CodingTracker.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly ILogger<ProjectsService> _logger;
    private readonly IProjectRepository _projectRepository;

    public ProjectsService(IProjectRepository projectRepository, ILogger<ProjectsService> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public void AddProject(AddProjectRequest projectRequest)
    {
        projectRequest.Name = AnsiConsole.Ask<string>("Enter the project name:");
        projectRequest.Description = AnsiConsole.Ask<string>("Enter the project description:");
        var languagesInput = AnsiConsole.Ask<string>(
            "Enter the programming languages used (comma-separated):"
        );

        if (!string.IsNullOrWhiteSpace(languagesInput))
        {
            projectRequest.ProgrammingLanguages ??= new List<string>();
            foreach (
                var lang in languagesInput
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())
            )
                projectRequest.ProgrammingLanguages.Add(lang);

            var project = projectRequest.ToProjectEntity();
            _projectRepository.AddProject(project);
            _logger.LogInformation("Project with ID: {ProjectId} added", project.ProjectId);
        }
    }

    public List<ProjectResponse> GetAllProjects()
    {
        var projects = _projectRepository.GetAllProjects();

        if (!projects.Any())
            AnsiConsole.MarkupLine("[Red]No projects to display. Please add a new project.[/]");

        var projectResponses = projects.Select(p => p.ToProjectResponse()).ToList();

        UserInterface.ViewAllProjects(projectResponses);
        return projectResponses;
    }

    public ProjectResponse? GetProject()
    {
        var projects = GetAllProjects();

        if (!projects.Any())
            return null;

        UserInterface.ViewAllProjects(projects);
        var project = Helpers.GetProjectById(projects);

        var projectResponse = _projectRepository.GetProject(project.Id).ToProjectResponse();
        _logger.LogInformation("Project with ID: {projectId} retrieved.", project);

        return projectResponse;
    }

    public void ViewProjectById()
    {
        var project = GetProject();

        if (project is null)
            return;

        UserInterface.ViewProjectDetails(project);
    }

    public void UpdateProject()
    {
        var projects = GetAllProjects();
        var projectResponse = Helpers.GetProjectById(projects);
        var project = _projectRepository.GetProject(projectResponse.Id);

        var updateProject = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Name", "Description", "Programming Languages Used")
        );
        switch (updateProject)
        {
            case "Name":
                project.Name = AnsiConsole.Ask<string>("Enter the project name:");
                break;
            case "Description":
                project.Description = AnsiConsole.Ask<string>("Enter the project description:");
                break;
            case "Programming Languages Used":
                var languagesInput = AnsiConsole.Ask<string>(
                    "Enter the programming languages used (comma-separated):"
                );

                if (!string.IsNullOrWhiteSpace(languagesInput))
                {
                    project.ProgrammingLanguages = new List<string>();
                    foreach (
                        var lang in languagesInput
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(l => l.Trim())
                    )
                        project.ProgrammingLanguages.Add(lang);
                }

                break;
        }

        _projectRepository.UpdateProject(project);
        _logger.LogInformation("Project with ID: {projectId} updated.", project.ProjectId);
        AnsiConsole.Clear();
        GetAllProjects();
    }

    public bool DeleteProject()
    {
        var projects = GetAllProjects();

        if (!projects.Any())
            return false;

        var projectResponse = Helpers.GetProjectById(projects);

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");
        _projectRepository.DeleteProject(projectResponse.Id);
        _logger.LogInformation("Project with ID: {projectId} deleted.", projectResponse);
        return true;
    }
}
