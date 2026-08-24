using System.ComponentModel.DataAnnotations;

namespace CodingTracker.Enums;

public enum ProjectMenuOptions
{
    [Display(Name = "Add Project")] AddProject,

    [Display(Name = "View All Projects")] ViewAllProjects,

    [Display(Name = "View Project")] ViewProject,

    [Display(Name = "Update Project")] UpdateProject,

    [Display(Name = "Delete Project")] DeleteProject,

    [Display(Name = "Back to Main Menu")] BackToMainMenu
}