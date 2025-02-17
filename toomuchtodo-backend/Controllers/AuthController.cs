using Microsoft.AspNetCore.Mvc;
using toomuchtodo_backend.Models;
using toomuchtodo_backend.Services;
using toomuchtodo_backend.Utils;
using toomuchtodo_backend.ViewModels;

namespace toomuchtodo_backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginViewModel login)
    {
        var user = _userRepository.GetByEmail(login.Email);

        if (user == null || !PasswordHasher.VerifyHashedPassword(login.Password, user.password))
        {
            return Unauthorized("Usuário ou senha incorretos");
        }

        var token = TokenService.GenerateToken(user);
        
        return Ok(token);
    }
}