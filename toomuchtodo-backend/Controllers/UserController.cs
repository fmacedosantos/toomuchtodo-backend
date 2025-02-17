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
    public IActionResult Add([FromBody] UserViewModel userViewModel)
    {
        if (_userRepository.GetByEmail(userViewModel.Email) != null)
            return Conflict("Email já cadastrado");

        if (_userRepository.GetByUsername(userViewModel.Username) != null)
            return Conflict("Username já cadastrado");
        
        var hashedPassword = PasswordHasher.HashPassword(userViewModel.Password);
        
        var user = new User(userViewModel.Username,userViewModel.Email, hashedPassword);
        
        _userRepository.Add(user);
        
        return Ok("Usuário cadastrado com sucesso");
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        int tokenUserId = int.Parse(User.FindFirst("userId")?.Value);

        if (id != tokenUserId)
            return Forbid();
        
        var user = _userRepository.GetById(id);
    
        if (user == null)
        {
            return NotFound();  
        }
    
        return Ok(user);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult UpdatePassword(int id, [FromBody] UpdatePasswordViewModel updatePasswordViewModel)
    {
        int tokenUserId = int.Parse(User.FindFirst("userId")?.Value);
        
        if (id != tokenUserId)
            return Forbid();
        
        var user = _userRepository.GetById(id);
        
        if (user == null)
            return NotFound();
        
        if (!PasswordHasher.VerifyHashedPassword(updatePasswordViewModel.OldPassword, user.password))
            return BadRequest("Senha atual incorreta");
        
        user.UpdatePassword(PasswordHasher.HashPassword(updatePasswordViewModel.NewPassword));
        
        _userRepository.Update(user);
        
        return Ok("Senha atualizada com sucesso");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        int tokenUserId = int.Parse(User.FindFirst("userId")?.Value);
        
        if (id != tokenUserId)
            return Forbid();
        
        var user = _userRepository.GetById(id);
        
        if (user == null)
            return NotFound();
        
        _userRepository.Delete(user);

        return Ok("Usuário deletado com sucesso");
    }
}