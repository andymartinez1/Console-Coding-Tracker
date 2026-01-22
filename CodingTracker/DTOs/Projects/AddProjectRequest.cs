using System.ComponentModel.DataAnnotations;
using CodingTracker.Models;

namespace CodingTracker.DTOs.Projects;

public class AddProjectRequest
{
    [Required(ErrorMessage = "Name cannot be empty.")]
    [StringLength(
        32,
        MinimumLength = 3,
        ErrorMessage = "Project name must be between 3 and 32 characters long"
    )]
    public string? Name { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Project name must be more than 100 characters long")]
    public string? Description { get; set; } = string.Empty;

    public List<string>? ProgrammingLanguages { get; set; }

    public Project ToProjectEntity()
    {
        return new Project()
        {
            Name = Name,
            Description = Description,
            ProgrammingLanguages = ProgrammingLanguages,
        };
    }
}
