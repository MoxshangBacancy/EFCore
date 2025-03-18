using System.ComponentModel.DataAnnotations;

namespace Day2_Task.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage="Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email {  get; set; }

        public bool IsDeleted {  get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
