using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Day2_Task.Models
{
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }

        [Required(ErrorMessage="ProductName is required")]
        public string Name { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
