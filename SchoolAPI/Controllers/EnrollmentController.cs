using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        ApplicationDbContext _context;
        public EnrollmentController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Enrollments> GetEnrollments()
        {
            return _context.Enrollments.ToList();
        }
        [HttpPost]
        public IActionResult PostEnrollment([FromBody] Enrollments enrollment)
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            var objectInDB = _context.Enrollments.FirstOrDefault(c => c.Id == id);
            _context.Enrollments.Remove(objectInDB);
            _context.SaveChanges();
            return Ok();
        }
    }
}
