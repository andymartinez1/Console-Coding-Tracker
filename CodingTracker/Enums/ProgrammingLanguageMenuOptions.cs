using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum ProgrammingLanguageMenuOptions
{
    [Display(Name = "Add Programming Language")]
    AddProgrammingLanguage,

    [Display(Name = "View All Programming Languages")]
    ViewAllProgrammingLanguages,

    [Display(Name = "View Programming Language")]
    ViewProgrammingLanguage,

    [Display(Name = "Update Programming Language")]
    UpdateProgrammingLanguage,

    [Display(Name = "Delete Programming Language")]
    DeleteProgrammingLanguage,

    [Display(Name = "Back to Main Menu")]
    BackToMainMenu,
}
