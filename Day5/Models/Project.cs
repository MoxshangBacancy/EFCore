using System.ComponentModel.DataAnnotations;

namespace Day5_Task.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

       
        public List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
