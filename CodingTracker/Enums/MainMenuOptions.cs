using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum MainMenuOptions
{
    [Display(Name = "Coding Sessions Menu")]
    CodingSessionMenu,

    [Display(Name = "Projects Menu")] ProjectMenu,

    [Display(Name = "Programming Languages Menu")]
    ProgrammingLanguageMenu,

    Exit
}