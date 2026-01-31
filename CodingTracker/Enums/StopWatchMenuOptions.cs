using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum StopWatchMenuOptions
{
    [Display(Name = "Start Timed Session")]
    StartTimedSession,

    [Display(Name = "Back to Sessions Menu")]
    BackToSessionsMenu
}