using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Category Category { get; set; }

    public List<ProgrammingLanguage> ProgrammingLanguages { get; set; }

    public List<CodingSession> CodingSessions { get; set; }
}

public enum Category
{
    Feature,
    Bugfix,
}
