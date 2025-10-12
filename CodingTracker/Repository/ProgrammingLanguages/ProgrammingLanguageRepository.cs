using CodingTracker.Data;
using CodingTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTracker.Repository.ProgrammingLanguages;

public class ProgrammingLanguageRepository : IProgrammingLanguageRepository
{
    private readonly CodingDbContext _dbContext;

    public ProgrammingLanguageRepository(CodingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertLanguage(ProgrammingLanguage language)
    {
        _dbContext.ProgrammingLanguages.Add(language);

        _dbContext.SaveChanges();
    }

    public List<ProgrammingLanguage> GetAllLanguages()
    {
        var languages = _dbContext.ProgrammingLanguages.Include(p => p.CodingSessions).ToList();

        return languages;
    }

    public ProgrammingLanguage GetLanguage(int id)
    {
        return _dbContext.ProgrammingLanguages.Find(id);
    }

    public void UpdateLanguage(ProgrammingLanguage language)
    {
        _dbContext.ProgrammingLanguages.Update(language);

        _dbContext.SaveChanges();
    }

    public void DeleteLanguage(int id)
    {
        var language = GetLanguage(id);

        _dbContext.ProgrammingLanguages.Remove(language);

        _dbContext.SaveChanges();
    }
}
