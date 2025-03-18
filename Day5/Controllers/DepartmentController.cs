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
    public class DepartmentController : ControllerBase
    {
        private readonly DBContext _context;

        public DepartmentController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("CreateDepartment")]
       
        public IActionResult CreateDepartment([FromBody] DepartmentDTO departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Invalid department data. 'DepartmentName' is required.");
            }

            if (_context.Departments.Any(d => d.DepartmentName == departmentDto.DepartmentName))
            {
                return Conflict($"A department with the name '{departmentDto.DepartmentName}' already exists.");
            }

            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName
            };

            //METHOD-1
            _context.Departments.Add(department);
            //METHOD-2
            //_context.Add(department);
            _context.SaveChanges();

            return Ok($"Department {departmentDto.DepartmentName} added");
        }

        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            var departments = _context.Departments
                .Select(d => new { d.DepartmentId, d.DepartmentName })
                .AsNoTracking()
                .ToList();

            if (!departments.Any())
            {
                return NotFound("No departments found.");
            }

            return Ok(departments);
        }


        [HttpGet("GetDepartmentsWithEmployeesById{id}")]
        public IActionResult GetDepartmentWithEmployeesById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid department ID.");
            }

            var department = _context.Departments
                .Include(d => d.Employees)
                .Where(d => d.DepartmentId == id)
                .Select(d => new
                {
                    d.DepartmentId,
                    d.DepartmentName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.EmployeeId,
                        EmployeeName = e.Name,
                        EmployeeEmail = e.Email
                    })
                });

            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            return Ok(department);
        }

        [HttpPut("UpdateDepartmentById/{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] DepartmentDTO updatedDepartment)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Department Id.");
            }

            if (updatedDepartment == null)
            {
                return BadRequest("Please enter department name");
            }

            var existingDepartment = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (existingDepartment == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            if (_context.Departments.Any(d => d.DepartmentName == updatedDepartment.DepartmentName && d.DepartmentId != id))
            {
                return Conflict($"A department with the name '{updatedDepartment.DepartmentName}' already exists.");
            }

            existingDepartment.DepartmentName = updatedDepartment.DepartmentName;

            //METHOD-1
            _context.Departments.Update(existingDepartment);

            //METHOD-2
            _context.SaveChanges();

            return Ok($"DEPARTMENT UPDATED TO {updatedDepartment.DepartmentName}");
        }

        [HttpDelete("DeleteDepartmentById/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid department ID.");
            }

            var department = _context.Departments.Include(d => d.Employees).FirstOrDefault(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }

            if (department.Employees.Any())
            {
                return BadRequest("Cannot delete department as it has associated employees. Remove employees first.");
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();

            return Ok($"Department {department.DepartmentName} delete");
        }



    }
}
