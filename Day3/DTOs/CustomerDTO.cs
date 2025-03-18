﻿namespace Day_3_Task.DTOs
{
    public class CustomerDTO
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime CreatedDate { get; set; }
            public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
        

    }
}
