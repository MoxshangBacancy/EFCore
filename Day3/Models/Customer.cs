using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day_3_Task.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public string Email { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string PhoneNumber { get; set; }

        //Navigation property for Many Orders
       
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
