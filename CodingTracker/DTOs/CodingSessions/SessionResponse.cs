using CodingTracker.Models;

namespace CodingTracker.DTOs.CodingSessions;

public class SessionResponse
{
    public int SessionId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public TimeSpan Duration => EndTime - StartTime;

    public string? Category { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; }
}

public static class SessionExtensions
{
    public static SessionResponse ToSessionResponse(this CodingSession session)
    {
        return new SessionResponse
        {
            SessionId = session.SessionId,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Category = session.Category,
            ProjectId = session.ProjectId,
            Project = session.Project
        };
    }
}