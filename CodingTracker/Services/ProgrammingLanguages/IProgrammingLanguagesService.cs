using CodingTracker.Models;

namespace CodingTracker.Services.ProgrammingLanguages;

public interface IProgrammingLanguagesService
{
    public void AddLanguage();

    public List<ProgrammingLanguage> GetAllLanguages();

    public ProgrammingLanguage GetLanguage();

    public void ViewLanguageById();

    public void UpdateLanguage();

    public void DeleteLanguage();
}
