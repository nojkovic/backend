using Backend.Models;
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
       public readonly UserContext _context;
       public UserController(IConfiguration config, UserContext context)
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
            
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Success");
       }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);

            if (existingUser != null)
            {
                if(existingUser.Password == user.Password)
                    return Ok(new JWTServices(_config).GenerateToken(
                        existingUser.Id.ToString(),
                        existingUser.Name,
                        existingUser.Email,
                        existingUser.LastName
                        ));
            }
            else
            {
                return Ok(new { message = "Incorect password" ,status="uncorect"});
            }
                
            return Ok(new { message = "User not found" ,status="unauthorization"});
            

        }
    }
}
