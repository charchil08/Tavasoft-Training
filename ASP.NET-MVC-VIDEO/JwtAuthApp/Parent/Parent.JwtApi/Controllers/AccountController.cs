using Microsoft.AspNetCore.Mvc;
using MvcJwt.WebApiJwt;

namespace Parent.JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       
        [HttpGet]
        public IActionResult ValidateLogin(string Username, string Password) 
        {
            if(Username == "admin" && Password == "admin")
            {
                return Ok(TokenManager.GenerateToken(Username));
            }
            return BadRequest("User not found");
        }
        

        [HttpGet("validate")]
        [CustomAuthenticationFilter]
        public IActionResult GetEmp()
        {
            return Ok("Hi");
        }
    }
}
