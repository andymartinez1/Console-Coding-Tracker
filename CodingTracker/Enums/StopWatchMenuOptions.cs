using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum StopWatchMenuOptions
{
    Start,
    Stop,
    Reset,
    ShowElapsedTime,

    [Display(Name = "Back to Main Menu")]
    BackToMainMenu,
}
