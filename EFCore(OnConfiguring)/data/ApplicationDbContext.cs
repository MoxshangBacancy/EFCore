using EFCore.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCore.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MOXSHANGSHAH\\SQLEXPRESS;Database=EmployeeConDb;ConnectRetryCount=0");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
