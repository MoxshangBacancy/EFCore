using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2_Task.Models
{
    public class Order
    {
        [Key]   
        public int OrderId {  get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
