using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Models;

public class CodingSession
{
    [Key]
    public int Id { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public TimeSpan Duration => EndTime - StartTime;
}
