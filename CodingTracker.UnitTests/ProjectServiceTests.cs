using CodingTracker.DTOs.Projects;
using CodingTracker.Models;
using CodingTracker.Services.Projects;
using Xunit;
using Assert = Xunit.Assert;

namespace CodingTracker.Tests;

public class ProjectServiceTests
{
    private readonly ProjectsService _projectsService;

    [Fact]
    public void AddProject_IfNotNull_AddsToDatabase()
    {
        // Arrange
        var projectRequest = new AddProjectRequest()
        {
            Name = "Test Project",
            Description = "This is just a test project",
            ProgrammingLanguages = ["C#", "JavaScript"],
        };

        // Act
        _projectsService.AddProject(projectRequest);

        // Assert
        Assert.NotNull(projectRequest);
    }

    [Fact]
    public void AddProject_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _projectsService.AddProject(null));
    }
}
