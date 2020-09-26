using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var user = _context.Users.FirstOrDefault(c => c.Id == id);
        //    if(user == null)
        //    {
        //        return Ok(user);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpPost]
        public IActionResult Post([FromBody] Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Put([FromBody] Users user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userInDb = _context.Users.Find(id);
            if(userInDb != null)
            {
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
