using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class Project
{
    [Key] public int ProjectId { get; set; }

    [MaxLength(50)] public string? Name { get; set; } = string.Empty;

    [MaxLength(200)] public string? Description { get; set; } = string.Empty;

    public List<string>? ProgrammingLanguages { get; set; } = [];

    public List<CodingSession>? CodingSessions { get; set; } = [];
}

