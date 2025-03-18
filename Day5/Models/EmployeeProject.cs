using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Day5_Task.Models
{
    public class EmployeeProject
    {
       
        public int EmployeeId { get; set; }

        
        public int ProjectId { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public Project Project { get; set; }

       
        public string Role { get; set; }
    }
}
