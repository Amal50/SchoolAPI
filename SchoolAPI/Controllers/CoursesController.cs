using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        ApplicationDbContext _context;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Courses> GetCourses()
        {
            return _context.Courses.ToList();
        }
        [HttpGet("{id}")]
        public IActionResult GetCourses(int id)
        {
            return Ok(_context.Courses.FirstOrDefault(c => c.Id == id));
        }
        [HttpPost]
        public IActionResult PostCourse([FromBody] Courses course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok();
        }
    }
}
