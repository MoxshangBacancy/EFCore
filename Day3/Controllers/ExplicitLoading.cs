using Microsoft.AspNetCore.Mvc;
using Day_3_Task.Data;
using Day_3_Task.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Day_3_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplicitLoading : ControllerBase
    {
        private readonly DBContext _context;

        public ExplicitLoading(DBContext context)
        {
            _context = context;
        }

        [HttpGet("GetCustomersWithOrders")]
        public IActionResult GetCustomerWithOrders(int customerId)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            
            if (customer.CreatedDate >= DateTime.UtcNow.AddYears(-1))
            {
                _context.Entry(customer).Collection(c => c.Orders).Load();
            }

            return Ok(new
            {
                customer.Id,
                customer.Name,
                customer.Email,
                Orders = customer.Orders.Select(o => new
                {
                    o.Id,
                    o.OrderDate
                }).ToList()
            });
        }

        [HttpGet("GetOrdersWithoutOrderProducts")]
        public IActionResult GetOrdersWithoutOrderProducts()
        {
            var orders = _context.Orders.ToList(); // No OrderProducts loaded yet

            var result = orders.Select(o => new
            {
                o.Id,
                o.OrderDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("GetProductsWithConditionalOrders")]
        public IActionResult GetProductsWithConditionalOrders()
        {
            var products = _context.Products.ToList();

            var result = products.Select(p =>
            {
                if (p.Stock < 10)
                {
                    _context.Entry(p).Collection(p => p.OrderProducts).Load();
                }

                return new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock,
                    Orders = p.Stock < 10
                        ? p.OrderProducts.Select(op => new { op.OrderId, op.Quantity }).ToList()
                        : null
                };
            }).ToList();

            return Ok(result);
        }

        [HttpGet("GetCustomersWithOrdersAndLoadOrderProducts")]
        public IActionResult GetCustomersWithOrdersAndLoadOrderProducts()
        {
            // Step 1: Eagerly load Customers with their Orders
            var customers = _context.Customers
                .Include(c => c.Orders) // Eager loading of Orders
                .ToList();

            // Step 2: Explicitly load OrderProducts for each Order
            foreach (var customer in customers)
            {
                foreach (var order in customer.Orders)
                {
                    _context.Entry(order).Collection(o => o.OrderProducts).Load();
                }
            }

            // Step 3: Prepare and return result
            var result = customers.Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                Orders = c.Orders.Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    OrderProducts = o.OrderProducts.Select(op => new
                    {
                        op.ProductId,
                        op.Quantity
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(result);
        }






    }
}
