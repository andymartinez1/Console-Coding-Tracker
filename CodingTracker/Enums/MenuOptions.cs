using System.ComponentModel.DataAnnotations;

namespace Coding_Tracker.Enums;

public enum MenuOptions
{
    [Display(Name = "Add Session")]
    AddSession,

    [Display(Name = "View Sessions")]
    ViewAllSessions,

    [Display(Name = "Update Session")]
    UpdateSession,

    [Display(Name = "Delete Session")]
    DeleteSession,

    [Display(Name = "Quit")]
    Quit,
}
