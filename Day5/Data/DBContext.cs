using System.Collections.Generic;
using System.Reflection.Emit;
using Day5_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Day5_Task.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });


            modelBuilder.Entity<Department>().HasData(
           new Department { DepartmentId = 1, DepartmentName = "HR" },
           new Department { DepartmentId = 2, DepartmentName = "IT" },
           new Department { DepartmentId = 3, DepartmentName = "Finance" },
           new Department { DepartmentId = 4, DepartmentName = "Marketing" },
           new Department { DepartmentId = 5, DepartmentName = "Operations" }
       );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Alice", Email = "alice@example.com", DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Bob", Email = "bob@example.com", DepartmentId = 2 },
                new Employee { EmployeeId = 3, Name = "Charlie", Email = "charlie@example.com", DepartmentId = 3 },
                new Employee { EmployeeId = 4, Name = "David", Email = "david@example.com", DepartmentId = 4 },
                new Employee { EmployeeId = 5, Name = "Eve", Email = "eve@example.com", DepartmentId = 5 }
            );

            modelBuilder.Entity<Project>().HasData(
            new Project { ProjectId = 1, ProjectName = "Project A", StartDate = new DateTime(2025, 03, 01) },
            new Project { ProjectId = 2, ProjectName = "Project B", StartDate = new DateTime(2025, 03, 05) },
            new Project { ProjectId = 3, ProjectName = "Project C", StartDate = new DateTime(2025, 03, 10) },
            new Project { ProjectId = 4, ProjectName = "Project D", StartDate = new DateTime(2025, 03, 15) },
            new Project { ProjectId = 5, ProjectName = "Project E", StartDate = new DateTime(2025, 03, 20) }
        );

            modelBuilder.Entity<EmployeeProject>().HasData(
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Manager" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 2, Role = "Developer" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 3, Role = "Analyst" },
                new EmployeeProject { EmployeeId = 4, ProjectId = 4, Role = "Designer" },
                new EmployeeProject { EmployeeId = 5, ProjectId = 5, Role = "Tester" }
            );
        }
    }
}




