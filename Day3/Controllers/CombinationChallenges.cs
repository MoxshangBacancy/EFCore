using Microsoft.AspNetCore.Mvc;
using Day_3_Task.Data;
using Day_3_Task.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Day_3_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationChallenges : ControllerBase
    {
        private readonly DBContext _context;

        public CombinationChallenges(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)  // Eagerly loads Customer
                .ToListAsync();            // OrderProducts are Lazy Loaded

            return Ok(orders);
        }



        //[HttpGet("{customerId}/orders")]
        //public async Task<IActionResult> GetCustomerOrdersWithExplicitLoading(int customerId)
        //{
        //    // Eagerly load the Customer with their Orders
        //    var customer = await _context.Customers
        //        .Include(c => c.Orders) // Eager loading Orders
        //        .FirstOrDefaultAsync(c => c.Id == customerId);

        //    if (customer == null)
        //    {
        //        return NotFound("Customer not found.");
        //    }

        //    // Explicitly load OrderProducts only if Customer is VIP
        //    if (customer.IsVIP)
        //    {
        //        foreach (var order in customer.Orders)
        //        {
        //            await _context.Entry(order)
        //                .Collection(o => o.OrderProducts)
        //                .LoadAsync();
        //        }
        //    }

        //    return Ok(customer);
        //}





    }
}
