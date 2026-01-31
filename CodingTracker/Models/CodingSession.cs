using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class CodingSession
{
    [Key] public int SessionId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public TimeSpan Duration => EndTime - StartTime;

    [MaxLength(50)] public string? Category { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; } = null;
}