using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class CodingSession
{
    [Key]
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public TimeSpan Duration => EndTime - StartTime;

    public Category? Category { get; set; }

    public Project Project { get; set; }

    public IEnumerable<ProgrammingLanguage> ProgrammingLanguages { get; set; }
}

public enum Category
{
    Feature,
    Bugfix,
    Refactor,
    Style,
    Test,
}
