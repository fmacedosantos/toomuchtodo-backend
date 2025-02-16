using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using toomuchtodo_backend.Models;
using toomuchtodo_backend.Utils;
using toomuchtodo_backend.ViewModels;

namespace toomuchtodo_backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    [HttpPost]
    public IActionResult Add([FromForm] UserViewModel userViewModel)
    {
        var hashedPassword = PasswordHasher.HashPassword(userViewModel.Password);
        
        var user = new User(userViewModel.Username,userViewModel.Email, hashedPassword);
        
        _userRepository.Add(user);
        
        return Ok("Usuário cadastrado com sucesso");
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userRepository.GetById(id);
    
        if (user == null)
        {
            return NotFound();  
        }
    
        return Ok(user);
    }
}