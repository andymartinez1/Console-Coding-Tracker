using CodingTracker.Models;

namespace CodingTracker.Data;

public static class SeedDatabase
{
    public static void SeedData(CodingDbContext context)
    {
        var programmingLanguages = new List<ProgrammingLanguage>
        {
            new() { Language = "C#", Version = 14 },
            new() { Language = "Java", Version = 25 },
            new() { Language = "JavaScript", Version = 15 },
            new() { Language = "TypeScript", Version = 5.9m },
            new() { Language = "Python", Version = 3.13m },
            new() { Language = "Go", Version = 1.25m },
            new() { Language = "Rust", Version = 1.91m },
            new() { Language = "Kotlin", Version = 2.2m },
        };

        var projects = new List<Project>
        {
            new()
            {
                Name = "Portfolio",
                Category = Category.Feature,
                Description = "Personal portfolio site and blog.",
            },
            new()
            {
                Name = "CLI Tool",
                Category = Category.Feature,
                Description = "Command-line productivity utilities.",
            },
            new()
            {
                Name = "Client Dashboard",
                Category = Category.Feature,
                Description = "Web dashboard for client analytics.",
            },
            new()
            {
                Name = "Library",
                Category = Category.Feature,
                Description = "Reusable open-source library.",
            },
        };

        var codingSessions = new List<CodingSession>
        {
            new()
            {
                StartTime = new DateTime(2025, 11, 18, 8, 30, 0),
                EndTime = new DateTime(2025, 11, 18, 10, 30, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0] },
                Project = projects[0],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 18, 17, 0, 0),
                EndTime = new DateTime(2025, 11, 18, 19, 30, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[2] },
                Project = projects[2],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 19, 9, 0, 0),
                EndTime = new DateTime(2025, 11, 19, 11, 15, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[3] },
                Project = projects[1],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 20, 14, 0, 0),
                EndTime = new DateTime(2025, 11, 20, 16, 45, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[4] },
                Project = projects[3],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 21, 18, 30, 0),
                EndTime = new DateTime(2025, 11, 21, 19, 0, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[0] },
                Project = projects[1],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 22, 7, 45, 0),
                EndTime = new DateTime(2025, 11, 22, 9, 0, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[6] },
                Project = projects[3],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 23, 12, 0, 0),
                EndTime = new DateTime(2025, 11, 23, 13, 30, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[5] },
                Project = projects[2],
            },
            new()
            {
                StartTime = new DateTime(2025, 11, 24, 20, 0, 0),
                EndTime = new DateTime(2025, 11, 24, 21, 0, 0),
                ProgrammingLanguages = new List<ProgrammingLanguage> { programmingLanguages[1] },
                Project = projects[0],
            },
        };

        context.ProgrammingLanguages.AddRange(programmingLanguages);
        context.Projects.AddRange(projects);
        context.CodingSessions.AddRange(codingSessions);
        context.SaveChanges();
    }
}
