using CodingTracker.Models;

namespace CodingTracker.Data;

public static class SeedDatabase
{
    public static void SeedData(CodingDbContext context)
    {
        var ProgrammingLanguages = new List<ProgrammingLanguage>
        {
            new ProgrammingLanguage() { Language = "C#" },
            new ProgrammingLanguage() { Language = "Java" },
            new ProgrammingLanguage() { Language = "JavaScript" },
        };

        var Projects = new List<Project>
        {
            new Project()
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
