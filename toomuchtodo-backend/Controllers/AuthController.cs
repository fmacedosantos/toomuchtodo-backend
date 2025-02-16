using Microsoft.AspNetCore.Mvc;
using toomuchtodo_backend.Models;
using toomuchtodo_backend.Services;

namespace toomuchtodo_backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    
    [HttpPost]
    public IActionResult Auth(string email, string password)
    {
        if (email == "admin" && password == "admin")
        {
            var token = TokenService.GenerateToken(new User(email, password));
            return Ok(token);
        }
        
        return BadRequest("Usuário ou senha incorretos");
    }
}