using CodingTracker.DTOs.Projects;
using CodingTracker.Repository;
using CodingTracker.Utils;
using CodingTracker.Views;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace CodingTracker.Services;

public class ProjectsService : IProjectsService
{
    private readonly ILogger<ProjectsService> _logger;
    private readonly IProjectRepository _projectRepository;

    public ProjectsService(IProjectRepository projectRepository, ILogger<ProjectsService> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task<ProjectResponse> AddAsync(AddProjectRequest? projectRequest)
    {
        projectRequest ??= new AddProjectRequest();

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
        }

        var project = projectRequest.ToProjectEntity();
        await _projectRepository.AddAsync(project);
        _logger.LogInformation("Project with ID: {ProjectId} added.", project.ProjectId);

        return project.ToProjectResponse();
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        if (!projects.Any())
            AnsiConsole.MarkupLine("[Red]No projects to display. Please add a new project.[/]");

        var projectResponses = projects.Select(p => p.ToProjectResponse()).ToList();

        UserInterface.ViewAllProjects(projectResponses);
        return projectResponses;
    }

    public async Task<ProjectResponse?> GetByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project is null)
            return null;

        var projectResponse = project.ToProjectResponse();
        _logger.LogInformation("Project with ID: {ProjectId} retrieved.", id);

        return projectResponse;
    }

    public async Task<ProjectResponse?> UpdateAsync(UpdateProjectRequest? request)
    {
        if (request is null)
            return null;

        var projects = await GetAllAsync();
        var projectResponse = Helpers.SelectProjectById(projects);
        var project = await _projectRepository.GetByIdAsync(projectResponse.Id);

        if (project is null)
            return null;

        var updateChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Name", "Description", "Programming Languages Used")
        );

        switch (updateChoice)
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

        await _projectRepository.UpdateAsync(project);
        _logger.LogInformation("Project with ID: {ProjectId} updated.", project.ProjectId);
        AnsiConsole.Clear();
        await GetAllAsync();

        return project.ToProjectResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var projects = await GetAllAsync();

        if (!projects.Any())
            return false;

        var projectResponse = Helpers.SelectProjectById(projects);

        await _projectRepository.DeleteAsync(projectResponse.Id);
        _logger.LogInformation("Project with ID: {ProjectId} deleted.", projectResponse.Id);
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");

        return true;
    }
}