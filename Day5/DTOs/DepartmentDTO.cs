using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Day5_Task.DTOs
{
    public class DepartmentDTO
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
