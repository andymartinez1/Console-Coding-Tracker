using CodingTracker.Models;
using CodingTracker.Services.Projects;
using Xunit;

namespace CodingTracker.Tests;

public class ProjectServiceTests
{
    private readonly ProjectsService _projectsService;

    [Fact]
    public void AddProject_IfNotNull_AddsToDatabase()
    {
        // Arrange
        var project = new Project
        {
            Name = "Test Project",
            Description = "This is just a test project",
            ProgrammingLanguages = ["C#", "JavaScript"],
        };

        // Act
        // _projectsService.AddProject();

        // Assert
    }
}
