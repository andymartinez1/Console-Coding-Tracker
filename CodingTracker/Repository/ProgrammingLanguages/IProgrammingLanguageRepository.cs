using CodingTracker.Models;

namespace CodingTracker.Repository.ProgrammingLanguages;

public interface IProgrammingLanguageRepository
{
    public void InsertLanguage(ProgrammingLanguage language);

    public List<ProgrammingLanguage> GetAllLanguages();

    public ProgrammingLanguage GetLanguage(int id);

    public void UpdateLanguage(ProgrammingLanguage language);

    public void DeleteLanguage(int id);
}
