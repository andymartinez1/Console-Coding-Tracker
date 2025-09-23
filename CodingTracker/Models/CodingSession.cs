using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class CodingSession
{
    [Key]
    public int Id { get; set; }

    public Project Project { get; set; }

    public ProgrammingLanguage Language { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public TimeSpan Duration => EndTime - StartTime;
}
