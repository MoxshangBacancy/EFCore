using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day_3_Task.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        
        public virtual Order Order { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]


        public virtual Product Product { get; set; }
    }
}
