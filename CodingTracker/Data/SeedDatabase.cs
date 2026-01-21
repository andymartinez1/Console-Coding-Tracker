using CodingTracker.Models;

namespace CodingTracker.Data;

public static class SeedDatabase
{
    public static void SeedData(CodingDbContext context)
    {
        var projects = new List<Project>
        {
            new()
            {
                Name = "Portfolio",
                Description = "Personal portfolio site and blog.",
                ProgrammingLanguages = ["C#", "React"],
            },
            new() { Name = "CLI Tool", Description = "Command-line productivity utilities." },
            new()
            {
                Name = "Client Dashboard",
                Description = "Web dashboard for client analytics.",
            },
            new() { Name = "Library", Description = "Reusable open-source library." },
        };

        var codingSessions = new List<CodingSession>
        {
            new()
            {
                StartTime = new DateTime(2025, 11, 18, 8, 30, 0),
                EndTime = new DateTime(2025, 11, 18, 10, 30, 0),
                Category = Category.Feature,
                ProgrammingLanguages = ["C#", "Python"],
                Project = projects[0],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 18, 17, 0, 0),
                EndTime = new DateTime(2025, 11, 18, 19, 30, 0),
                Category = Category.Bugfix,
                ProgrammingLanguages = ["C#"],
                Project = projects[2],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 19, 9, 0, 0),
                EndTime = new DateTime(2025, 11, 19, 11, 15, 0),
                Category = Category.Refactor,
                ProgrammingLanguages = ["Java"],
                Project = projects[1],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 20, 14, 0, 0),
                EndTime = new DateTime(2025, 11, 20, 16, 45, 0),
                Category = Category.Test,
                ProgrammingLanguages = ["Python"],
                Project = projects[3],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 21, 18, 30, 0),
                EndTime = new DateTime(2025, 11, 21, 19, 0, 0),
                Category = Category.Style,
                ProgrammingLanguages = ["Java"],
                Project = projects[1],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 22, 7, 45, 0),
                EndTime = new DateTime(2025, 11, 22, 9, 0, 0),
                Category = Category.Feature,
                ProgrammingLanguages = ["C#"],
                Project = projects[3],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 23, 12, 0, 0),
                EndTime = new DateTime(2025, 11, 23, 13, 30, 0),
                Category = Category.Bugfix,
                ProgrammingLanguages = ["Java"],
                Project = projects[2],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 24, 20, 0, 0),
                EndTime = new DateTime(2025, 11, 24, 21, 0, 0),
                Category = Category.Test,
                ProgrammingLanguages = ["C#"],
                Project = projects[0],
            },
        };

        context.Projects.AddRange(projects);
        context.CodingSessions.AddRange(codingSessions);
        context.SaveChanges();
    }
}
