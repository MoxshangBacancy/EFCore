using System.ComponentModel.DataAnnotations;

namespace Day5_Task.DTOs
{
    public class EmployeeDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
