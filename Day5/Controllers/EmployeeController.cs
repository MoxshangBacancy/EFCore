using Microsoft.AspNetCore.Mvc;
using Day5_Task.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day5_Task.Models;
using Day5_Task.DTOs;

namespace Day5_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContext _context;

        public EmployeeController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("AddEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is required");
            }

            if(_context.Employees.Any(e => e.Email == employeeDto.Email))
            {
                return Conflict("An employee with given email already exists");
            }

            if (_context.Departments.All(d => d.DepartmentId != employeeDto.DepartmentId))
            {
                return NotFound("DeaprtmentID is not found");
            }

            var employee = new Employee
            {
                Email = employeeDto.Email,
                Name = employeeDto.Name,
                DepartmentId = employeeDto.DepartmentId
            };
            //Method-1 
            _context.Employees.Add(employee);

            //Mehtod-2
            //_context.Add(employee);
            _context.SaveChanges();

            return Ok($"Employee {employeeDto.Name} added");
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _context.Employees.Select(e => new {
               EmployeeID =  e.EmployeeId,
                EmployeeName = e.Name,
                DepartmentID = e.DepartmentId
            })
            .AsNoTracking()
            .ToList();

            if (!employees.Any())
            {
                return NotFound("No employees found.");
            }

            return Ok(employees);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.EmployeeProjects)
                    .ThenInclude(ep => ep.Project)
                .Where(e => e.EmployeeId == id)
                .Select(e => new
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.Name,
                    EmployeeEmail = e.Email,
                    e.DepartmentId,
                    e.Department.DepartmentName,
                    EmployeeProjects = e.EmployeeProjects.Select(p => new
                    {
                        p.Project.ProjectName,
                        p.Role,
                        p.Project.StartDate
                    })
                });

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }

        [HttpPut("UpdateEmployeeById/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDTO updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return BadRequest("Invalid employee data.");
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            if (_context.Departments.All(d => d.DepartmentId != updatedEmployee.DepartmentId))
            {
                return NotFound($"Department with ID {updatedEmployee.DepartmentId} not found.");
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.DepartmentId = updatedEmployee.DepartmentId;

            _context.SaveChanges();
            return Ok($"Employee {id} updated ");
        }

        [HttpDelete("DeleteEmployeeById/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok($"Employee {id} deleted");
        }
    }
}
    


        
    

