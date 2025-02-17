using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace toomuchtodo_backend.Models;

[Table("tasks")]
public class TaskItem
{
    [Key]
    public int id { get; private set; }
    public int user_id { get; private set; }
    [Required]
    public string title { get; private set; }
    public string? description { get; private set; }
    public bool is_completed { get; private set; } = false;
    public bool is_important { get; private set; } = false;
    public DateTime created_at { get; private set; } = DateTime.UtcNow;
    public DateTime updated_at { get; private set; } = DateTime.UtcNow;


    public TaskItem(int user_id, string title, string? description = null)
    {
        this.user_id = user_id;
        this.title = title;
        this.description = description;
        this.created_at = DateTime.UtcNow;
        this.updated_at = DateTime.UtcNow;
    }
    
    public void Update(string title, string? description)
    {
        this.title = title;
        this.description = description;
        this.updated_at = DateTime.UtcNow;
    }
    
    public void MarkAsImportant(bool important)
    {
        this.is_important = important;
        this.updated_at = DateTime.UtcNow;
    }
    
    public void MarkAsCompleted(bool completed)
    {
        this.is_completed = completed;
        this.updated_at = DateTime.UtcNow;
    }
}