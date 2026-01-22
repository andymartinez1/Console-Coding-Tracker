using CodingTracker.Models;

namespace CodingTracker.DTOs.Projects;

public class ProjectResponse
{
    public int Id { get; set; }

    public string? Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public List<string>? ProgrammingLanguages { get; set; } = [];

    public List<CodingSession> CodingSessions { get; set; } = [];
}

public static class ProjectExtensions
{
    public static ProjectResponse ToProjectResponse(this Project project)
    {
        return new ProjectResponse
        {
            Id = project.ProjectId,
            Name = project.Name,
            Description = project.Description,
            ProgrammingLanguages = project.ProgrammingLanguages,
            CodingSessions = project.CodingSessions,
        };
    }
}
