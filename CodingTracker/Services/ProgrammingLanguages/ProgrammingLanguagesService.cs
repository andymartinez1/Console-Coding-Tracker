using CodingTracker.Models;
using CodingTracker.Repository.ProgrammingLanguages;
using CodingTracker.Utils;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Services.ProgrammingLanguages;

public class ProgrammingLanguagesService : IProgrammingLanguagesService
{
    private readonly IProgrammingLanguageRepository _languageRepository;

    public ProgrammingLanguagesService(IProgrammingLanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public void AddLanguage()
    {
        var language = new ProgrammingLanguage();

        language.Language = AnsiConsole.Ask<string>("Enter the programming language: ");
        language.Version = AnsiConsole.Ask<decimal?>("Enter the version number: ");

        _languageRepository.AddLanguage(language);
    }

    public List<ProgrammingLanguage> GetAllLanguages()
    {
        var languages = _languageRepository.GetAllLanguages();

        if (!Validation.IsListEmpty(languages))
            UserInterface.ViewAllLanguages(languages);
        else
        {
            AnsiConsole.MarkupLine(
                "[Red]No programming languages to display. Please add a new language.[/]"
            );
        }

        return languages;
    }

    public ProgrammingLanguage? GetLanguage()
    {
        var languages = GetAllLanguages();

        if (!Validation.IsListEmpty(languages))
        {
            UserInterface.ViewAllLanguages(languages);
            var languageId = Helpers.GetLanguageById(languages);
            return _languageRepository.GetLanguage(languageId);
        }

        return null;
    }

    public void ViewLanguageById()
    {
        var language = GetLanguage();

        if (language == null)
            return;

        UserInterface.ViewLanguageDetails(language);
    }

    public void UpdateLanguage()
    {
        var languageToUpdate = GetLanguage();

        if (languageToUpdate == null)
        {
            return;
        }

        var updateProgrammingLanguageName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the programming language name? ")
                .AddChoices("Yes", "No")
        );
        if (updateProgrammingLanguageName == "Yes")
            languageToUpdate.Language = AnsiConsole.Ask<string>("Enter the programming language");

        var updateProgrammingLanguageVersionNumber = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Would you like to update the programming language version number? ")
                .AddChoices("Yes", "No")
        );
        if (updateProgrammingLanguageVersionNumber == "Yes")
            languageToUpdate.Version = AnsiConsole.Ask<decimal?>("Enter the version number");

        _languageRepository.UpdateLanguage(languageToUpdate);
    }

    public void DeleteLanguage()
    {
        var languages = GetAllLanguages();

        var languageId = Helpers.GetLanguageById(languages);

        if (!Validation.IsListEmpty(languages))
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[green]Session deleted successfully![/]");
            _languageRepository.DeleteLanguage(languageId);
        }
    }
}
