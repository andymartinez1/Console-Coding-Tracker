using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum SessionMenuOptions
{
    [Display(Name = "Start a Timed Session")]
    StartSession,

    [Display(Name = "Add Session")]
    AddSession,

    [Display(Name = "View All Sessions")]
    ViewAllSessions,

    [Display(Name = "View Session")]
    ViewSession,

    [Display(Name = "Update Session")]
    UpdateSession,

    [Display(Name = "Delete Session")]
    DeleteSession,

    [Display(Name = "Back to Main Menu")]
    BackToMainMenu,
}
