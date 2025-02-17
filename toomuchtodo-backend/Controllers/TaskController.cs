using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using toomuchtodo_backend.Models;
using toomuchtodo_backend.Persistence;
using toomuchtodo_backend.ViewModels;

namespace toomuchtodo_backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskViewModel model)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = new TaskItem(userId, model.Title, model.Description);
        
        _taskRepository.Add(task);
        
        return Ok("Tarefa criada com sucesso");
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var tasks = _taskRepository.GetByUserId(userId);
        
        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = _taskRepository.GetById(id);
    
        if (task == null)
            return NotFound("Tarefa não encontrada");
    
        if (task.user_id != userId)
            return Forbid();
    
        return Ok(task);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateTaskViewModel model)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = _taskRepository.GetById(id);
        
        if(task == null) return NotFound("Tarefa não encontrada");
        
        if(task.user_id != userId) return Forbid();
        
        task.Update(model.Title, model.Description);
        
        _taskRepository.Update(task);
        
        return Ok("Tarefa atualizada com sucesso");
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = _taskRepository.GetById(id);
        
        if(task == null) return NotFound("Tarefa não encontrada");
        
        if(task.user_id != userId) return Forbid();
        
        _taskRepository.Delete(task);
        
        return Ok("Tarefa deletada com sucesso");
    }
    
    [HttpPut("{id}/important")]
    public IActionResult MarkImportant(int id, [FromBody] MarkImportantViewModel model)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = _taskRepository.GetById(id);
        
        if(task == null) return NotFound("Tarefa não encontrada");
        
        if(task.user_id != userId) return Forbid();
        
        task.MarkAsImportant(model.IsImportant);
        
        _taskRepository.Update(task);
        
        return Ok("Status de importância atualizado com sucesso");
    }
    
    [HttpPut("{id}/complete")]
    public IActionResult MarkCompleted(int id, [FromBody] MarkCompletedViewModel model)
    {
        int userId = int.Parse(User.FindFirst("userId")?.Value);
        
        var task = _taskRepository.GetById(id);
        
        if(task == null) return NotFound("Tarefa não encontrada");
        
        if(task.user_id != userId) return Forbid();
        
        task.MarkAsCompleted(model.IsCompleted);
        
        _taskRepository.Update(task);
        
        return Ok("Status de conclusão atualizado com sucesso");
    }
}