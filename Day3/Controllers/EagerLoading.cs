using Day_3_Task.Data;
using Day_3_Task.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day_3_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EagerLoading : ControllerBase
    {
        private readonly DBContext _context;

        public EagerLoading(DBContext context)
        {
            _context = context;
        }

        [HttpGet("CustomersWithOrdersAndOrderProducts")]
        public IActionResult GetCustomersWithOrdersAndOrderProducts()
        {
            var customers = _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product) 
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    CreatedDate = c.CreatedDate,
                    Orders = c.Orders.Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        IsDeleted = o.IsDeleted,
                        CustomerId = o.CustomerId,
                        OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
                        {
                            Id = op.Id,
                            OrderId = op.OrderId,
                            ProductId = op.ProductId,
                            Quantity = op.Quantity,
                            Product = new ProductDTO
                            {
                                Id = op.Product.Id,
                                Name = op.Product.Name,
                                Price = op.Product.Price,
                                Stock = op.Product.Stock
                            }
                        }).ToList()
                    }).ToList()
                })
                .ToList();

            return Ok(customers);
        }

        [HttpGet("RecentCustomersWithOrdersAndOrderProducts")]
        public IActionResult GetRecentCustomersWithOrdersAndOrderProducts()
        {
            var customers = _context.Customers
    .Include(c => c.Orders)
        .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
    .Select(c => new CustomerDTO
    {
        Id = c.Id,
        Name = c.Name,
        Email = c.Email,
        CreatedDate = c.CreatedDate,
        Orders = c.Orders
            .Where(o => o.OrderDate >= DateTime.Now.AddDays(-30)) 
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                IsDeleted = o.IsDeleted,
                CustomerId = o.CustomerId,
                OrderProducts = o.OrderProducts
                    .Where(op => op.Product.Stock > 20)
                    .Select(op => new OrderProductDTO
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Product = new ProductDTO
                        {
                            Id = op.Product.Id,
                            Name = op.Product.Name,
                            Price = op.Product.Price,
                            Stock = op.Product.Stock
                        }
                    }).ToList()
            }).ToList()
    })
    .Where(c => c.Orders.Any())
    .ToList();

            return Ok(customers);
        }


        [HttpGet("products-with-orders")]
        public IActionResult GetProductsWithOrderCount()
        {
            var productsWithOrderCount = _context.Products
                .Include(p => p.OrderProducts) 
                .Select(p => new
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    TotalOrders = p.OrderProducts.Count 
                })
                .ToList();

            return Ok(productsWithOrderCount); 
        }

        [HttpGet("recent-orders")]
        public IActionResult GetOrdersPlacedInLastMonth()
        {
            var oneMonthAgo = DateTime.Now.AddMonths(-1); 

            var recentOrders = _context.Orders
                .Include(o => o.Customer) 
                .Where(o => o.OrderDate >= oneMonthAgo) 
                .Select(o => new
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    Customer = new
                    {
                        o.Customer.Id,
                        o.Customer.Name,
                        o.Customer.Email
                    }
                })
                .ToList();

            return Ok(recentOrders); 
        }











    }
}
