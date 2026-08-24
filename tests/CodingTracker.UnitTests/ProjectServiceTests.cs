using CodingTracker.DTOs.Projects;
using CodingTracker.Services.Projects;
using Moq;
using Xunit;

namespace CodingTracker.Tests;

public class ProjectServiceTests
{
    private readonly Mock<IProjectsService> _mockProjectsService;

    public ProjectServiceTests()
    {
        _mockProjectsService = new Mock<IProjectsService>();
    }

    [Fact]
    public void AddProject_IfNotNull_CallsServiceWithRequest()
    {
        // Arrange
        var projectRequest = new AddProjectRequest
        {
            Name = "Test Project",
            Description = "This is just a test project",
            ProgrammingLanguages = ["C#", "JavaScript"]
        };

        // Act
        _mockProjectsService.Object.AddProject(projectRequest);

        // Assert
        _mockProjectsService.Verify(s => s.AddProject(projectRequest), Times.Once);
    }

    [Fact]
    public void AddProject_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        _mockProjectsService
            .Setup(s => s.AddProject(null!))
            .Throws<ArgumentNullException>();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _mockProjectsService.Object.AddProject(null!));
    }
}