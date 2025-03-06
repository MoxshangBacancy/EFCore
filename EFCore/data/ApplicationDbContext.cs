using Microsoft.EntityFrameworkCore;
using EFCore.Models.Entity;

namespace EFCore.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)  
            {
                optionsBuilder.UseSqlServer("Server=MOXSHANGSHAH\\SQLEXPRESS;Database=EmployeeOnConfigDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
