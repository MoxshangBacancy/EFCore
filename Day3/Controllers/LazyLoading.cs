using Microsoft.AspNetCore.Mvc;
using Day_3_Task.Data;
using Day_3_Task.DTOs;

namespace Day_3_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LazyLoading : ControllerBase
    {
        private readonly DBContext _context;

        public LazyLoading(DBContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomersWithOrders")]
        public IActionResult GetCustomersWithOrders()
        {
            var customers = _context.Customers.ToList(); 

            var result = customers.Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                Orders = c.Orders.Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    TotalAmount = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
                }).ToList() 
            }).ToList();

            return Ok(result);
        }

        [HttpGet("GetHighValueCustomers")]
        public IActionResult GetHighValueCustomers()
        {
            var customers = _context.Customers.ToList();

            var result = customers.Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                Orders = c.Orders
                    .Where(o => o.OrderProducts.Sum(op => op.Quantity * op.Product.Price) > 500)
                    .Select(o => new
                    {
                        o.Id,
                        o.OrderDate,
                        TotalAmount = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
                    }).ToList() 
            }).Where(c => c.Orders.Any()).ToList(); 

            return Ok(result);
        }




    }
}
