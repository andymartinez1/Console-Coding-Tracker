using CodingTracker.Models;
using CodingTracker.Repository.Projects;
using CodingTracker.Utils;
using CodingTracker.Views;
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
        var project = new Project();

        project.Name = AnsiConsole.Ask<string>("Enter the project name:");
        project.Description = AnsiConsole.Ask<string>("Enter the project description:");
    }

    public List<Project> GetAllProjects()
    {
        var projects = _projectRepository.GetAllProjects();

        if (!Validation.IsListEmpty(projects))
        {
            UserInterface.ViewAllProjects(projects);
        }
        else
        {
            AnsiConsole.MarkupLine("[Red]No projects to display. Please add a new project.[/]");
        }

        return projects;
    }

    public Project? GetProject()
    {
        var projects = GetAllProjects();

        if (!Validation.IsListEmpty(projects))
        {
            UserInterface.ViewAllProjects(projects);
            var projectId = Helpers.GetProjectById(projects);
            return _projectRepository.GetProject(projectId);
        }

        return null;
    }

    public void ViewProjectById()
    {
        var project = GetProject();

        if (project == null)
            return;

        UserInterface.ViewProjectDetails(project);
    }

    public void UpdateProject()
    {
        var projectToUpdate = GetProject();

        if (projectToUpdate == null)
            return;

        var updateProjectName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the project name? ")
                .AddChoices("Yes", "No")
        );
        if (updateProjectName == "Yes")
            projectToUpdate.Name = AnsiConsole.Ask<string>("Enter the project name:");

        var updateProjectDescription = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the project description? ")
                .AddChoices("Yes", "No")
        );
        if (updateProjectDescription == "Yes")
            projectToUpdate.Description = AnsiConsole.Ask<string>("Enter the project description:");

        _projectRepository.UpdateProject(projectToUpdate);
    }

    public void DeleteProject()
    {
        var projects = GetAllProjects();

        var projectId = Helpers.GetProjectById(projects);

        if (!Validation.IsListEmpty(projects))
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");
            _projectRepository.DeleteProject(projectId);
        }
    }
}
