using Microsoft.AspNetCore.Mvc;
using Day2_Task.Models;
using Day2_Task.Data;
using Microsoft.EntityFrameworkCore;

namespace Day2_Task.Controllers
    
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly EFCoreDbContext _context;
        
        public CustomerController(EFCoreDbContext context)
        {
            _context = context;
        }

        [HttpGet] //get all customers 
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);  
        }

        [HttpPost] // Create customer checking entity Customer state
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            } 

         
            if (_context.Customers.Any(c => c.CustomerName == customer.CustomerName))
            {
                return BadRequest("Customer with this name already exists.");
            }

           var state = _context.Entry(customer).State;
            _context.Customers.Add(customer);

            state = _context.Entry(customer).State;
            await _context.SaveChangesAsync();

            state = _context.Entry(customer).State;

            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
        }

        


        [HttpPut("{id}")] // Update by Customer id
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if(id!= updatedCustomer.CustomerId)
            {
                return BadRequest("Customer Id is mismatched");
            }
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return BadRequest("Not found");
            }
            customer.CustomerName = updatedCustomer.CustomerName;
            customer.Email = updatedCustomer.Email;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("hard-delete/{id}")] // Delete by id 
        public async Task<IActionResult> DeleteHardCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpDelete("soft-delete/{id}")] // Delete by id 
        public async Task<IActionResult> DeleteSoftCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }



            customer.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(customer);
        }



    }
}
