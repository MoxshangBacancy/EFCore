using System.ComponentModel.DataAnnotations;

namespace Day5_Task.DTOs
{
    public class ProjectDTO
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }
    }
}
