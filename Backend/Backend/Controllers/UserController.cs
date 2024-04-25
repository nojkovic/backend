using Backend.Models;
using Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AngularAPP")]
    public class UserController : ControllerBase
    {
       private readonly IConfiguration _config;
       public readonly VisasContext _context;
       public UserController(IConfiguration config, VisasContext context)
        {
            _config = config;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
       public IActionResult Create(User user)
       {
            if (_context.Users.Select(x => x.Email == user.Email).FirstOrDefault() )
            {
                return Ok("Already exist");
            }
            user.Password= BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Success");
       }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var response = new ResponseLog();
            try
            {
                var existingUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);
                

                if (existingUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
                    {
                        response.Message = "Login successful";
                        response.Status = "success";
                        response.JwtToken = new JWTServices(_config).GenerateToken(existingUser);
                        return Ok(response);
                    }
                    else
                    {
                        response.Message = "Password does not match";
                        return StatusCode(400, response);
                    }
                }
                else
                {
                    response.Message = "User not found";
                    return StatusCode(404, response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return StatusCode(500, response);
            }

        }
    }



}
