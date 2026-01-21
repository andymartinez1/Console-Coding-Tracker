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
        var languagesInput = AnsiConsole.Ask<string>(
            "Enter the programming languages used (comma-separated):"
        );

        if (!string.IsNullOrWhiteSpace(languagesInput))
        {
            project.ProgrammingLanguages ??= new List<string>();
            foreach (
                var lang in languagesInput
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())
            )
            {
                project.ProgrammingLanguages.Add(lang);
            }

            _projectRepository.AddProject(project);
        }
    }

    public List<Project> GetAllProjects()
    {
        var projects = _projectRepository.GetAllProjects();

        if (projects.Any())
            UserInterface.ViewAllProjects(projects);
        else
            AnsiConsole.MarkupLine("[Red]No projects to display. Please add a new project.[/]");

        return projects;
    }

    public Project? GetProject()
    {
        var projects = GetAllProjects();

        if (projects.Any())
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

        var updateProject = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to update?")
                .AddChoices("Name", "Description", "Programming Languages Used")
        );
        switch (updateProject)
        {
            case "Name":
                projectToUpdate.Name = AnsiConsole.Ask<string>("Enter the project name:");
                break;
            case "Description":
                projectToUpdate.Description = AnsiConsole.Ask<string>(
                    "Enter the project description:"
                );
                break;
            case "Programming Languages Used":
                var languagesInput = AnsiConsole.Ask<string>(
                    "Enter the programming languages used (comma-separated):"
                );

                if (!string.IsNullOrWhiteSpace(languagesInput))
                {
                    projectToUpdate.ProgrammingLanguages ??= new List<string>();
                    foreach (
                        var lang in languagesInput
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(l => l.Trim())
                    )
                    {
                        projectToUpdate.ProgrammingLanguages.Add(lang);
                    }
                }
                break;
        }

        _projectRepository.UpdateProject(projectToUpdate);
        AnsiConsole.Clear();
        GetAllProjects();
    }

    public void DeleteProject()
    {
        var projects = GetAllProjects();

        var projectId = Helpers.GetProjectById(projects);

        if (projects.Any())
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");
            _projectRepository.DeleteProject(projectId);
        }
    }
}
