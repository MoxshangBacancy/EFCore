namespace Day_3_Task.DTOs
{
    public class ProductDTO
    {
     
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public List<OrderProductDTO> OrderProducts { get; set; } = new List<OrderProductDTO>();
        

    }
}
