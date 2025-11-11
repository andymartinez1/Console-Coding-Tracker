using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum StopWatchMenuOptions
{
    [Display(Name = "Start Timer")]
    Start,

    [Display(Name = "Stop Timer")]
    Stop,

    [Display(Name = "Show Elapsed Time")]
    ShowElapsedTime,

    [Display(Name = "Reset Timer")]
    Reset,

    [Display(Name = "Back to Sessions Menu")]
    BackToSessionsMenu,
}
