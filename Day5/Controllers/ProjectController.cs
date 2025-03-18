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
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly DBContext _context;

        public ProjectController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("CreateProject")]
        public IActionResult CreateProject([FromBody] ProjectDTO projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is required.");
            }

            var project = new Project
            {
                ProjectName = projectDto.ProjectName,
                
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Ok($"PROJECT {projectDto.ProjectName} ADDED");
        }

        [HttpGet("GetAllProjects")]
        public IActionResult GetAllProjects()
        {
            var projects = _context.Projects.Select(p => new {
                p.ProjectId,
                p.ProjectName,
                p.StartDate
            })
            .AsNoTracking()
            .ToList();

            if (!projects.Any())
            {
                return NotFound("No projects found.");
            }

            return Ok(projects);
        }

        [HttpGet("GetProjectById/{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _context.Projects
                .Include(p => p.EmployeeProjects)
                    .ThenInclude(ep => ep.Employee)
                .Where(p => p.ProjectId == id)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.StartDate,
                    EmployeeProjects = p.EmployeeProjects.Select(ep => new
                    {
                        ep.EmployeeId,
                        EmployeeName = ep.Employee.Name,
                        EmployeeEmail = ep.Employee.Email
                    })
                });

            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(project);
        }

        

        [HttpPut("UpdateProjectById/{id}")]
        public IActionResult UpdateProject(int id, [FromBody] ProjectDTO updatedProject)
        {
            if (updatedProject == null)
            {
                return BadRequest("Invalid project data.");
            }

            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            project.ProjectName = updatedProject.ProjectName;
            

            _context.SaveChanges();
            return Ok(project);
        }

        [HttpDelete("DeleteProjectById/{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return Ok($"PROJECT {id} DELETED");
        }




    }
}
