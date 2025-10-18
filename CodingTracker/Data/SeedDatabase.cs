using CodingTracker.Models;

namespace CodingTracker.Data;

public static class SeedDatabase
{
    public static void SeedData(CodingDbContext context)
    {
        var ProgrammingLanguages = new List<ProgrammingLanguage>
        {
            new() { Language = "C#" },
            new() { Language = "Java" },
            new() { Language = "JavaScript" },
        };

        var Projects = new List<Project>
        {
            new()
            {
                Name = "Portfolio",
                Category = Category.Feature,
                Description = "Portfolio site for developer",
            },
        };

        context.ProgrammingLanguages.AddRange(ProgrammingLanguages);
        context.Projects.AddRange(Projects);
        context.SaveChanges();
    }
}
