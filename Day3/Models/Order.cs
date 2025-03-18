using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day_3_Task.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }


        public int CustomerId { get; set; }


        [ForeignKey("CustomerId")]

     
        public virtual Customer Customer { get; set; }


        public bool IsDeleted { get; set; } = false;

      
        public virtual List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
