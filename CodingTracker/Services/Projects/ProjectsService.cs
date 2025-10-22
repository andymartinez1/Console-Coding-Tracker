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
        Project project = new Project();

        project.Name = AnsiConsole.Ask<string>("Enter the project name:");
        project.Description = AnsiConsole.Ask<string>("Enter the project description:");
    }

    public List<Project> GetAllProjects()
    {
        return _projectRepository.GetAllProjects();
    }

    public Project GetProject()
    {
        var projects = GetAllProjects();

        if (!projects.Any())
            AnsiConsole.MarkupLine(
                "[Red]No coding sessions to display. Please add new session.[/]"
            );

        UserInterface.ViewAllProjects(projects);

        var projectId = Helpers.GetProjectById(projects);

        return _projectRepository.GetProject(projectId);
    }

    public void UpdateProject()
    {
        var projectToUpdate = GetProject();

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

        if (_projectRepository.GetAllProjects().Count > 0)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");
        }

        _projectRepository.DeleteProject(projectId);
    }
}
