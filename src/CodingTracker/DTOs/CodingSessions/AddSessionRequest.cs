using CodingTracker.Enums;
using CodingTracker.Models;

namespace CodingTracker.DTOs.CodingSessions;

public class AddSessionRequest
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Category Category { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; } = null;

    public CodingSession ToSessionEntity()
    {
        return new CodingSession
        {
            StartTime = StartTime,
            EndTime = EndTime,
            Category = Category.ToString(),
            ProjectId = ProjectId,
            Project = Project
        };
    }
}