using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        ApplicationDbContext _context;
        public TokenController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        [Route("{userName}/{password}")]
        public IActionResult Post(string userName, string password)
        {
            Users user = _context.Users.FirstOrDefault(c => c.UserName == userName && c.Password == password); 
            if(user == null)
            {
                return Unauthorized();
            }

            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AFDABSECRETKEYAMAL"));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new
                JwtSecurityToken(
                                "MyProject",
                                "MyClient",
                                claims,
                                expires: DateTime.Now.AddMinutes(30),
                                signingCredentials: signingCredentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
