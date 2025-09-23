namespace CodingTracker.Models;

public class ProgrammingLanguage
{
    public int Id { get; set; }

    public string Language { get; set; } = string.Empty;

    public decimal Version { get; set; }

    public List<CodingSession> CodingSessions { get; set; }
}
