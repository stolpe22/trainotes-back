using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;

namespace trainote_api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "teste" && password == "teste")
            {
                var token = TokenService.GenerateToken(new Models.Usuario());
                return Ok(token);
            }
            return BadRequest("username or password invalid");
        }
    }
}