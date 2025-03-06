using Microsoft.EntityFrameworkCore;
using EFCore.Models.Entity;

namespace EFCore.data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)  
        //    {
        //        optionsBuilder.UseSqlServer("Server=MOXSHANGSHAH\\SQLEXPRESS;Database=EmployeeOnConfigDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //    }
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

                string connectionString = environment switch
                {
                    "Development" => "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=DevDb;Trusted_Connection=True;TrustServerCertificate=True;",
                    "Staging" => "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=StagingDb;Trusted_Connection=True;TrustServerCertificate=True;",
                    "Production" => "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=ProdDb;Trusted_Connection=True;TrustServerCertificate=True;",
                    _ => "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=DefaultDb;Trusted_Connection=True;TrustServerCertificate=True;"
                };

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
