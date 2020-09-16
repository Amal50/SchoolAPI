using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        ApplicationDbContext _context;
        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Students> GetStudents()
        {
            return _context.Students.ToList();
        }
        [HttpPost]
        public IActionResult PostStudent([FromBody] Students student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok();
        }
    }
}
