using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Day2_Task.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public int OrderId { get; set; }

        //Navigate Property
        [ForeignKey("OrderId")]
        public Order Order { get; set; } 

        [Required]
        public int ProductId { get; set; }

        // Navigate Property
        [ForeignKey("ProductId")]
        public Product Product { get; set; } 
    }
}
